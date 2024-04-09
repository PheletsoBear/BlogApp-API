using Blog.API.Models.Domain;
using Blog.API.Models.DTO;
using Blog.API.Models.DTO.BlogPost;
using Blog.API.Repositories.Implemetation;
using Blog.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostDTO request)
        {
            //Convert DTO to Domain Model
            var blogPost = new BlogPost
            {
                Title = request.Title,
                ShortDesc = request.ShortDesc,
                Content = request.Content,
                FeaturedImgUrl = request.FeaturedImgUrl,
                UrlHandle = request.UrlHandle,
                PublishDate = request.PublishDate,
                Author = request.Author,
                IsVisible = request.IsVisible,
                Categories = new List<Category>() //creating a new instance of Category List

            };

            foreach (var categoryGuid in request.Categories)
            {

                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }




            //Below code comes from the constructor ontop with that injected the interface

            await blogPostRepository.CreateAsync(blogPost);

            var response = new BlogPostDTO
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDesc = blogPost.ShortDesc,
                Content = blogPost.Content,
                FeaturedImgUrl = blogPost.FeaturedImgUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishDate = blogPost.PublishDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()



            };

            return Ok(response);
        }





        [HttpGet]
        public async Task<IActionResult> GetBlogPosts()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();

            var response = new List<BlogPostDTO>();
            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDTO
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    ShortDesc = blogPost.ShortDesc,
                    Content = blogPost.Content,
                    FeaturedImgUrl = blogPost.FeaturedImgUrl,
                    UrlHandle = blogPost.UrlHandle,
                    Author = blogPost.Author,
                    PublishDate = blogPost.PublishDate,
                    IsVisible = blogPost.IsVisible,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()

                });
            }
            return Ok(response);

        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var existingBlogPost = await blogPostRepository.GetByIdAsync(id);
            if (existingBlogPost == null)
            {
                return NotFound();
            }

            var response = new BlogPostDTO
            {
                Id = existingBlogPost.Id,
                Title = existingBlogPost.Title,
                ShortDesc = existingBlogPost.ShortDesc,
                Content = existingBlogPost.Content,
                FeaturedImgUrl = existingBlogPost.FeaturedImgUrl,
                UrlHandle = existingBlogPost.UrlHandle,
                Author = existingBlogPost.Author,
                PublishDate = existingBlogPost.PublishDate,
                IsVisible = existingBlogPost.IsVisible,
                Categories = existingBlogPost.Categories.Select(
                 x => new CategoryDto
                 {
                     Id = x.Id,
                     Name = x.Name,
                     UrlHandle = x.UrlHandle
                 }).ToList()

            };
            return Ok(response);
        }


        [HttpGet]
        [Route ("{urlHandle}")]
        public async Task<IActionResult> GetBlogPostByUrl([FromRoute] string urlHandle)
        {
            var blogPost = await blogPostRepository.GetByUrlAsync(urlHandle);

            if(blogPost == null)
            {
                return NotFound();
            }

            var response = new BlogPostDTO
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDesc = blogPost.ShortDesc,
                Content = blogPost.Content,
                FeaturedImgUrl = blogPost.FeaturedImgUrl,
                UrlHandle = blogPost.UrlHandle,
                Author = blogPost.Author,
                PublishDate = blogPost.PublishDate,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(
                x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()

            };
            return Ok(response);

        }





        [HttpPut]
        [Route ("{id:Guid}")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, UpdateBlogPostDTO request)
        {
            //Mapping  DTO To Domain-model
            var BlogPost = new BlogPost
            {
                Id = id,
                Title = request.Title,
                ShortDesc = request.ShortDesc,
                Content = request.Content,
                FeaturedImgUrl = request.FeaturedImgUrl,
                UrlHandle = request.UrlHandle,
                PublishDate = request.PublishDate,
                Author = request.Author,
                IsVisible = request.IsVisible,
                Categories = new List<Category>() //creating a new instance of Category List

            };


            foreach (var categoryGuid in request.Categories)
            {

                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if (existingCategory is not null)
                {
                    BlogPost.Categories.Add(existingCategory);
                }
            }

            BlogPost = await blogPostRepository.UpdateAsync(BlogPost);

            if(BlogPost == null)
            {
                return NotFound();
            }

            //Mapping  DTO To Domain-model
            var response = new BlogPostDTO
            {
                Id = id,
                Title = BlogPost.Title,
                ShortDesc = BlogPost.ShortDesc,
                Content = BlogPost.Content,
                FeaturedImgUrl = BlogPost.FeaturedImgUrl,
                UrlHandle = BlogPost.UrlHandle,
                PublishDate = BlogPost.PublishDate,
                Author = BlogPost.Author,
                IsVisible = BlogPost.IsVisible,

                Categories = BlogPost.Categories.Select(
                 x => new CategoryDto
                 {
                     Id = x.Id,
                     Name = x.Name,
                     UrlHandle = x.UrlHandle
                 }).ToList()

            };

            return Ok(response);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var blogPost = await blogPostRepository.DeleteAsync(id);

            if (blogPost is null)
            {
                return NotFound();
            }

            // Mapping Model to DTO 
            var response = new BlogPostDTO
            {
                Id = id,
                Title = blogPost.Title,
                ShortDesc = blogPost.ShortDesc,
                Content = blogPost.Content,
                FeaturedImgUrl = blogPost.FeaturedImgUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishDate = blogPost.PublishDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible
              

            };
            return Ok(response);

        }

    }
}
