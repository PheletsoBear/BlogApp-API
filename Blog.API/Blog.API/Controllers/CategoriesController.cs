using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Models.DTO;
using Blog.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
 {
    /*
        This controller is being accesed by the below URL:

           https://localhost:xxxx/api/categories
     */
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
            //Map DTO to Domain Model by which we link request DTO to the  Domain model

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

        //api/categories
        [HttpGet]

        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();
            await categoryRepository.GetAllAsync();

            //Map Domain Model to DTO

            var response = new List<CategoryDto>();
            foreach(var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle

                });
            }
           return Ok(response);
        }
    }


}

