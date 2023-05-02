using BusinessEntities.RequestDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Repositories.Interface
{
    public interface IJWTAuthenticaitonManagerRepository
    {
       Task<User> Authentiate(string username,string password);
    }
}
