using Blog.API.Models.DTO.Authentication;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Microsoft.AspNet.Identity.EntityFramework.IdentityUser> userManager;

        public AuthController(UserManager<Microsoft.AspNet.Identity.EntityFramework.IdentityUser> userManager)
        {
            this.userManager = userManager;
        }


      


    } 

}
