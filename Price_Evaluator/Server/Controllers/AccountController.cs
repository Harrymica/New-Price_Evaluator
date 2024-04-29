using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Price_Evaluator.Server.Database;
using Price_Evaluator.Server.Services;
using System.Security.Cryptography;
using System.Text;

namespace Price_Evaluator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IRegisterServices _service;

        const int keySize = 20;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        private string? logMessage;


        public AccountController(DataContext context, IRegisterServices service)
        {
            _context = context;
            _service = service;
        }
        [HttpPost("{login}")]
        public async Task<ActionResult<Login>> LoginUser(Login _login)
        {

            var user = _context.Users.FirstOrDefault(u => u.Email == _login.Email);
            byte[] salt;
            string hashedPassword = HashPasword(_login.Password, out salt);

            bool isSuccess = VerifyPassword(_login.Password, user.Password, user.StoredSalt);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            if (user != null && user.StoredSalt != null)
            {
                var result = new UserModel
                {
                    Email = user.Email
                };


                if (isSuccess)
                {

                    return Ok("welcome Back");
                }


                else
                {

                    return BadRequest("Invalid login attempt.");
                }
            }


            return Ok("Welcome Back");
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(UserModel user)
        {
            byte[] salt;
            string hashedPassword = HashPasword(user.Password, out salt);


            bool isSuccess = await SaveUserToDatabase(user, hashedPassword, salt);

            return Ok(user);
        }

        //use this for custom authentication dont touch
        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserModel>> GetProfile(string email)
        {
            var userProfile = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return Ok(userProfile);
        }

        
        [HttpPost("admin")]
        public async Task<ActionResult<Login>> AdminLogin(Login login)
        {
            if (login != null)
            {

               
                bool CheckAdmin = await _context.Users.AnyAsync(u => u.Email == login.Email && u.Password == login.Password);
                if (CheckAdmin == true)
                {

                    return Ok();
                }
                else
                {
                    return BadRequest("You're not the admin");
                }
            }
            return Ok();
        }

        [HttpGet("user")]
        public async Task<ActionResult<List<UserModel>>> ViewUsers()
        {
           
            var viewAllUsers = await _context.Users.ToListAsync(); //.FirstOrDefaultAsync(u => u.Email != user);
                return Ok(viewAllUsers);
           
        }
        // PUT api/<AccountController>/5
        [HttpPut("edit/{Edithuser}")]
        public async Task<ActionResult<UserModel>> EditUser(int Id, UserModel user)
        {
            //user = await _context.Users.FindAsync(Id);
            var Finduser = await _context.Users.FindAsync(user.Id);
            
            if (Finduser == null)
                return NotFound("User not found");

            Finduser.FirstName = user.FirstName;
            Finduser.LastName = user.LastName;
            Finduser.PhoneNumber = user.PhoneNumber;
            Finduser.Email= user.Email;
            Finduser.Role = user.Role;


            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("Delete/{Id}")]
        public async Task<ActionResult<UserModel>> Delete(int Id)
        {
            var Finduser = await _context.Users.FindAsync(Id);
            if (Finduser == null)
                return NotFound("User not found");

            _context.Remove(Finduser);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        private string HashPasword(string password, out byte[] salt)
        {


            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
                hashAlgorithm,
                keySize);


            return Convert.ToHexString(hash);


        }
        private bool VerifyPassword(string password, string storedHash, byte[] storedSalt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, storedSalt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(storedHash));
        }
        private async Task<bool> SaveUserToDatabase(UserModel register, string hashPassword, byte[] salt)
        {

            string hashedPassword = HashPasword(register.Password, out salt);


            register.Password = hashedPassword;
            register.StoredSalt = salt;
            _context.Users.Add(register);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
