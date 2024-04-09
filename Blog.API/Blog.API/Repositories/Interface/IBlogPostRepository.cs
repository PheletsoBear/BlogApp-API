using Blog.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;



namespace Blog.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<BlogPost?> GetByUrlAsync(string url);

        Task<BlogPost?> UpdateAsync(BlogPost post);

        Task<BlogPost?> DeleteAsync(Guid id);



    }
}
