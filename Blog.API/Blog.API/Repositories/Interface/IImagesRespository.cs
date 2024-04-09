using Blog.API.Models.Domain;
using Blog.API.Models.DTO.BlogPost;

namespace Blog.API.Repositories.Interface
{
    public interface IImagesRespository
    {
        Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);
        Task<IEnumerable<BlogImage>> GetAllAsync();
       
    }
}
