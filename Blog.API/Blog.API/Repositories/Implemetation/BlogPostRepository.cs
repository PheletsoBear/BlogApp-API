using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositories.Implemetation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext DbContext)
        {
            this.dbContext = DbContext;
        }


        // Creating a BlogPost
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {

            var existingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBlogPost is null)
            {
                return null;
            }
            else
            {

            dbContext.BlogPosts.Remove(existingBlogPost);
            await dbContext.SaveChangesAsync();
            return existingBlogPost;

            }
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {

            return await dbContext.BlogPosts.Include(x => x.Categories).ToListAsync(); //this line gets the blogPost alongside the categories since both entities are related
        }

        public  Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public  Task<BlogPost?> GetByUrlAsync(string url)
        {
            return dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == url);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost post)
        {
            var existingBlogPost = await dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.Id == post.Id);

            if (existingBlogPost != null)
            {
                dbContext.Entry(existingBlogPost).CurrentValues.SetValues(post);
                await dbContext.SaveChangesAsync(true);
                return post;
            }
            else
            {
                return null;

            }
        }
    }
}
