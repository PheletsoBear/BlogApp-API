using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Data
{
    public class AuthDbContext: IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //intializing Id's for two roles
            var readerRoleId = "94c46341-b4e7-4630-b3cc-9f90236402d8";
            var writerRoleId = "5e25be48-e639-4354-ae51-24addc6e1009";
           
            
            //create reader and Writer Roles

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(), //This proprty is for Normalizing the database to avoid redundacy
                    ConcurrencyStamp = readerRoleId //This property is used to avoid concurrency and deadlocks
                },

                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),//This proprty is for Normalizing the database to avoid redundacy
                    ConcurrencyStamp = writerRoleId //This property is used to avoid concurrency and deadlocks
                }
            };

            //seed roles
            builder.Entity<IdentityRole>().HasData(roles); //Ensures that when EF Core migration runs it seeds data for the role

            //Create default Admin user
            var adminUserId = "5959f6ec-90f0-4f76-8868-5a97202638fe";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "Admin@BlogApp.com",
                Email = "Admin@BlogApp.com",
                NormalizedEmail = "Admin@BlogApp.com".ToUpper(),
                NormalizedUserName = "Admin@BlogApp.com".ToUpper()
            };

            //Create Hashed Password
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "MyPassword@128");

            builder.Entity<IdentityUser>().HasData(admin);

            //Gives roles to Admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new IdentityUserRole<string>()
                {
                        UserId = adminUserId,
                        RoleId = writerRoleId
                }
            };


            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

               
        }




    }
}
