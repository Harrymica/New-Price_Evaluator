using Microsoft.EntityFrameworkCore;
using Price_Evaluator.Server.Database;
using System.Security.Cryptography;
using System.Text;

namespace Price_Evaluator.Server.Services
{
    public class RegisterService : IRegisterServices
    {
        private readonly DataContext _context;
        const int keySize = 20;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        private string? logMessage;
        bool isSuccessful = true;

        public RegisterService(DataContext context)
        {
            _context = context;
        }
        public async Task<UserModel> Login(Login _login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == _login.Email);
            var result = new UserModel
            {
                Email = user.Email
            };
            if (_login.Email != null && _login.Password != null)
            {

                byte[] salt;
                string hashedPassword = HashPasword(_login.Password, out salt);
                if (user != null)
                {

                    bool isSuccess = VerifyPassword(_login.Password, user.Password, user.StoredSalt);

                    if (isSuccess)
                    {

                        //await lStorage.SetItemAsync<string>("email", _login.Email);
                        //await Authprov.GetAuthenticationStateAsync();




                    }

                }

            }

            return result;

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

        public Task Register(UserModel user)
        {
            throw new NotImplementedException();
        }
    
}
}
