using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Models.DTO.BlogPost;
using Blog.API.Repositories.Interface;

namespace Blog.API.Repositories.Implemetation
{
    public class imagesRespository : IImagesRespository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext dbContext;

        public imagesRespository(IWebHostEnvironment webHostEnvironment, 
            IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            //1-Upload the Image to API/Images local path

            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");
            //uploading the image tp the physical path we created "localpath"
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            //2-Update the Database
            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";


            blogImage.Url = urlPath;

            await dbContext.BlogImages.AddAsync(blogImage);
            await dbContext.SaveChangesAsync();
            return blogImage;


        }

        public Task<BlogImageDTO> Upload(IFormFile file, BlogImageDTO blogImage)
        {
            throw new NotImplementedException();
        }
    }
}
