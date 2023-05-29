using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusinessServices.Interface
{
    public interface IJWTAuthenticaitonManagerService
    {
       Task<LoginResponse> Authentiate(string mobileNo,string password);
    }
}
