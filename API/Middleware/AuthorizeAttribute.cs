using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace API.JWTMiddleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        private readonly string _someFilterParameter;
        public CustomAuthorizeAttribute(string someFilterParameter)
        {
            _someFilterParameter = someFilterParameter;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
           
                var account = context.HttpContext.Items["User"];
                if (account == null)
                {
                    // not logged in
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
           

        }
    }
}