using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositories.Implemetation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext; 

        //Injecting DbContext into the Repository
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;

        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
          return  await dbContext.Categories.ToListAsync();
        }
    }
}
