﻿using BusinessEntities.ResponseDto;
namespace API.Helpers
{
    public static class Common
    {
        public static LoginResponse GetSessionData(HttpContext context)
        {
            var id = context.User.Claims.First(c => c.Type == "Id");
            var email = context.User.Claims.First(c => c.Type == "Email");
            var mobileNo = context.User.Claims.First(c => c.Type == "MobileNo");
            var Address = context.User.Claims.First(c => c.Type == "Address");
            var name = context.User.Identity?.Name;
            //var username = context.User.Claims.First(c => c.Type == "Username");
            return new LoginResponse
            {
                Id = Convert.ToInt16(id.Value),
                Email = email.Value,
                MobileNo = mobileNo.Value,
                Address = Address.Value,
                FirstName = name
               // Username = username.Value,

            };
        }
     
    }
}
