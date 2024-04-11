using Azure.Identity;
using Blog.API.Models.DTO.Authentication;
using Blog.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRespository tokenRespository;

        public AuthController(UserManager<IdentityUser> userManager,
                              ITokenRespository tokenRespository)
        {
            this.userManager = userManager;
            this.tokenRespository = tokenRespository;
        }

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] loginRequestDTO request)
        {

            var identityUser = await userManager.FindByEmailAsync(request.Email);

            if (identityUser != null)
            {
                //check password
                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);

                if (checkPasswordResult)
                {
                   var roles = await userManager.GetRolesAsync(identityUser);
                    //create a token and response
                    var JwtToken =  tokenRespository.CreateJwtToken(identityUser, roles.ToList());

                    var response = new loginResponseDTO()
                    {

                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = JwtToken
                    };

                    return Ok(response);
                }

            }
            else
            {
                ModelState.AddModelError("", "Email or Password Incorrect");
            }

            return ValidationProblem(ModelState);
        }


        [HttpPost]
        [Route("register")]
      public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {

            //Create IdentityUser Object

            var user = new Microsoft.AspNetCore.Identity.IdentityUser
            {
                UserName = request.Email?.Trim(), //Get rids from any white space in the inpu field
                Email = request.Email?.Trim()//Get rids from any white space in the inpu field
            };

            //Creates a user with user object and and password
            var identityResult =  await userManager.CreateAsync(user, request.Password); 
           
            if (identityResult.Succeeded)
            {
                //Assign readerRole to the user
               identityResult =  await userManager.AddToRoleAsync(user, "Reader");

                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }

            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach(var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        
        }


    } 

}
