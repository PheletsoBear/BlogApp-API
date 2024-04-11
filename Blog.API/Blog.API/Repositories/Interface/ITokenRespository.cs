

using Microsoft.AspNetCore.Identity;

namespace Blog.API.Repositories.Interface
{
    public interface ITokenRespository
    {

        string CreateJwtToken(IdentityUser user, List<string> roles);

    }
}
