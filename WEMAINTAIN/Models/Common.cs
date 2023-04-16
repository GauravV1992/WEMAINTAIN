using BusinessEntities.ResponseDto;
namespace WEMAINTAIN.Models
{
    public static class Common
    {
        public static string GetAccessToken(HttpContext context)
        {
            string accessToken = string.Empty;
            if (context.Request.Cookies["access_token"] != null)
            {
                accessToken = context.Request.Cookies["access_token"];
            }
            return accessToken;
            //var id = context.User.Claims.First(c => c.Type == "Id");
            //var email = context.User.Claims.First(c => c.Type == "Email");
            //var mobileNo = context.User.Claims.First(c => c.Type == "MobileNo");
            //var Address = context.User.Claims.First(c => c.Type == "Address");
            //return new LoginResponse
            //{
            //    Id = Convert.ToInt16(id.Value),
            //    Email = email.Value,
            //    MobileNo = mobileNo.Value,
            //    Address = Address.Value,
            //    accessToken = toekn
            //};
        }
    }
}
