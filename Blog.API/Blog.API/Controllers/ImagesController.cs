using Blog.API.Models.Domain;
using Blog.API.Models.DTO;
using Blog.API.Models.DTO.BlogPost;
using Blog.API.Repositories.Implemetation;
using Blog.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesRespository imagesRespository;

        public ImagesController(IImagesRespository imagesRespository)
        {
            this.imagesRespository = imagesRespository;
        }

    [HttpGet]
    public async Task<IActionResult> GetAllImages()
        {
            //call repository to get all images
            var images = await imagesRespository.GetAllAsync();

            //Convert domain-model to DTO

            var response = new List<BlogImageDTO>();
           foreach (var image in images)
            {
                response.Add(new BlogImageDTO
                {
                    Id = image.Id,
                    Title = image.Title,
                    DateCreated = image.DateCreated,
                    FileExtension = image.FileExtension,
                    FileName = image.FileName,
                    Url = image.Url,
                });
            }
                return Ok(response);


        }



        //Post: {apiBaseUrl}/api/images
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file,
            [FromForm] string fileName, [FromForm] string title ) //We are getting info from the form in terms of file type and content type
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                //file upload to Domain-model
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now

                };

                blogImage = await imagesRespository.Upload(file, blogImage);

                //convert Domain-model to DTO

                var response = new BlogImageDTO
                {
                    Id = blogImage.Id,
                    Title = blogImage.Title,
                    DateCreated = DateTime.Now,
                    FileExtension = blogImage.FileExtension,
                    FileName = blogImage.FileName,
                    Url = blogImage.Url
                };

                return Ok(blogImage);

            }
            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtension = new string[] { ".png", ".jpeg", ".png",".jpg", ".jfif" };

            if (!allowedExtension.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");

            }
            if (file.Length > 10485770)
            {
                ModelState.AddModelError("file", "File size size cannot be more than 10Mb");
            }

        }
    }


}
