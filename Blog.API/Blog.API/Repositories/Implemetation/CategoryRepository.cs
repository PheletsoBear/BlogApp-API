﻿using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositories.Implemetation
{
    public class CategoryRepository : ICategoryRepository  //Inheriting the Interface repository in the implementation
    {
        //Injecting DbContext into the Repository
       
        
        private readonly ApplicationDbContext dbContext;   //Read-only property to inject the DbContext class

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Creating Category
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;

        }

        //Deletes category by id
        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            
            if (existingCategory is null)
            {
                return null;
            }
           //Below is just a basic else block in ASP.Net
                dbContext.Categories.Remove(existingCategory);
                await dbContext.SaveChangesAsync();
                return existingCategory;
           
        
        }


        //Get All the categories
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
          return  await dbContext.Categories.ToListAsync();
        }

        //Gets a category based on ID
        public async Task<Category?> GetById([FromRoute] Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id); // This returns the Id Based on the Lambda expression
        }



        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

                if (existingCategory != null)
            {
                dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync(true);
                return category;
            }
            else
            {
               return null;

            }
        }
    }
}
