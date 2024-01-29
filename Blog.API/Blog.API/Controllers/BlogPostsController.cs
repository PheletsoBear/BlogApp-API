using Blog.API.Models.Domain;
using Blog.API.Models.DTO;
using Blog.API.Repositories.Implemetation;
using Blog.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostsController( IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost (CreateBlogPostDTO request)
        {

            var blogPost = new BlogPost
            {
                Title = request.Title,
                ShortDesc = request.ShortDesc,
                Content = request.Content,
                FeaturedImgUrl = request.FeaturedImgUrl,
                UrlHandle = request.UrlHandle,
                PublishDate = request.PublishDate,
                Author = request.Author,
                IsVisible = request.IsVisible




            };
            //Below code comes from the constructor ontop with that injected the interface
 
                await blogPostRepository.CreateAsync(blogPost);

            var response = new BlogPostDTO
            {
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
