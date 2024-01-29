using Blog.API.Models.Domain;

namespace Blog.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);





    }
}
