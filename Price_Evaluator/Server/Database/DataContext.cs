using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Price_Evaluator.Server.Database
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("DbConnectionString"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    Id = 1,
                    Email = "Admin007@gmail.com",
                    FirstName = "Emmanuel",
                    LastName = "Chi",
                    PhoneNumber = "08878399001",
                    Password = "Admin@007",
                    Role = "Admin"
                    
                    

                }
                );
            
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        

    }
}
