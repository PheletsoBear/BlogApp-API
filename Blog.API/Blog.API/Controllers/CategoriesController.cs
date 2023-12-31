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

        //Injecting ICategoryRepository inside the Controller
        public CategoriesController(ICategoryRepository categoryRepository) {
            this.categoryRepository = categoryRepository;
        }



        //Creating Category

        [HttpPost] //This decorates controller action methods and is asociated with the HHTPS post method
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            //Mapping DTO to Domain Model by which we link request DTO to the  Domain model

            var category = new Category  // creating new category as an instance of the Category class in the Domain-model
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };


            await categoryRepository.CreateAsync(category);  // this ensures that the that the code does not run to the next line until Asynchronous method CreatAsync is finished

            // Mapping Domain model to DTO by which we link the newly created category to the DTO
            var response = new CategoryDto // creating response as in instance of the DTO 
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);



        }



        //Getting all the Categories

        // Get:https://localhost:7223/api/Categories
        [HttpGet]

        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();


            //Map Domain Model to DTO  in order to be able to list the categories

            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle

                });
            }
            return Ok(response); // This is the OK 200 success response that gets populated on the Swagger Documentation
        }

        //this is method for getting the category by id for eiting

        // Get:https://localhost:7223/api/Categories/id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoById([FromRoute] Guid id)
        {
            var existingCategory = await categoryRepository.GetById(id); //The extistingCategory variable will hold the value returned by the GetById method

            if (existingCategory is null)
            {
                return NotFound(); //this generates error status 404 not found result
            }


            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };
            return Ok(response);
        }

        //This is the method for updating the categories

        //PUT: https://localhost:7223/api/categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, UpdateCategoryDto request)
        {
            //Mapping  DTO To Domain-model

            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            category = await categoryRepository.UpdateAsync(category);

            if (category == null)
            {
                return NotFound();
            }
            //Convert Domain model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }



        //DELETE:  https://localhost:7223/api/categories/{id}
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteCategory ([FromRoute] Guid id)
        {
            var category = await categoryRepository.DeleteAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            
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

