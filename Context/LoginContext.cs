using Login_Register_Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Login_Register_Api.Context
{
    public class LoginContext:DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> options) : base(options) 
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}
