
using Blog.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

/*
    -In this module note that the DbContext class represents the the tables and 
    entity relationships as ASP.Net classes as Dbset peroperties
    -This will allow us to perform CRUD operation from SWAGGER or Angular VIEW

 */

namespace Blog.API.Data
{
    public class ApplicationDbContext : DbContext  //This inherits subclass of DbContext and create custom databae from Entity Framework
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //Passes the configuration options that defines the how the database context should be configured
        {
        }


        public DbSet<BlogPost> BlogPosts { get; set; } //Entity BlogPosts will be created in the MSSQL and this comes from domain models
        public DbSet<Category> Categories { get; set; }//Entity Categories will be created in the MSSQL and this comes from domain models 
        public DbSet<BlogImage> BlogImages { get; set; } //Entity BlogImages will be created in the MSSQL and this comes from domain models
    }
}
