using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using WEMAINTAIN.Models;

namespace WEMAINTAIN.Areas.Client.Components
{
    public class SignInSignUpViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SignInSignUpViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = new UserRequest();
            return View(user);
        }
    }
}

