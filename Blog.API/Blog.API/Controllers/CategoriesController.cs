using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Models.DTO;
using Blog.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    //https://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository) {
            this.categoryRepository = categoryRepository;
        }
       




        [HttpPost] //This decorates controller action methods and is asociated with the HHTPS post method
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            //Map DTO to Domain Model

            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle



            };


            await categoryRepository.CreateAsync(category);
            // Domain model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);



        }

    }
}

