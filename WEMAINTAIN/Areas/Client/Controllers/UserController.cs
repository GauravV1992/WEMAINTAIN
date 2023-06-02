using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using WEMAINTAIN.Models;
namespace WEMAINTAIN.Areas.Client.Controllers
{

    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public UserController(ILogger<UserController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View("~/areas/Client/views/Home.cshtml");
        }

        [HttpGet]
        public ActionResult Create(UserRequest request)
        {
            var Users = new UserRequest();
            return PartialView("~/areas/Client/views/Home.cshtml", Users);
        }

        [HttpPost]
        public async Task<ActionResult> Save(UserRequest request)
        {
            var response = new ResultDto<long>();
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                httpClient.DefaultRequestHeaders.Add(
              HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
                var httpResponseMessage = await httpClient.PostAsJsonAsync("User/Save", request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
            /*return*/
            RedirectToAction("Index", "Home");
            return Json(response);
        }


        [HttpGet]
        //public async Task<ActionResult> Edit(int id)
        //{
        //    var Users = new ResultDto<UserResponse>();
        //    var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
        //    httpClient.DefaultRequestHeaders.Add(
        //      HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
        //    var httpResponseMessage = await httpClient.GetAsync("User/GetById/" + id + "");
        //    if (httpResponseMessage.IsSuccessStatusCode)
        //    {
        //        var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
        //        Users = JsonSerializer.Deserialize<ResultDto<UserResponse>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //    }
            
        //    //return PartialView("~/areas/Client/views/MyProfile.cshtml", Users?.Data);
        //   return RedirectToAction("Index","MyProfile");
        //    //return Json(Users);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Update(UserRequest request)
        //{
        //    var response = new ResultDto<long>();
        //    if (ModelState.IsValid)
        //    {
        //        var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
        //        httpClient.DefaultRequestHeaders.Add(
        //      HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
        //        var httpResponseMessage = await httpClient.PostAsJsonAsync("User/Update", request);
        //        if (httpResponseMessage.IsSuccessStatusCode)
        //        {
        //            var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
        //            response = JsonSerializer.Deserialize<ResultDto<long>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //        }
        //    }
        //    return Json(response);
        //}



        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var token = string.Empty;
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            var httpResponseMessage = await httpClient.PostAsJsonAsync("User/Authenticate", model);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                token = await httpResponseMessage.Content.ReadAsStringAsync();
                Response.Cookies.Append("access_token", token, new CookieOptions()
                {
                    HttpOnly = true,
                    SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                    Secure = true
                });
            }
            return Json(httpResponseMessage.IsSuccessStatusCode);
        }

    }
}