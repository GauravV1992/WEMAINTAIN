using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WEMAINTAIN.Areas.Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public LoginController(ILogger<LoginController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View("~/Areas/Client/Views/Login.cshtml");
        }

        [HttpGet("SignOut")]
        public ActionResult SignOut()
        {
            Response.Cookies.Delete("access_token", new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true
            });
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Login");
            }
            var token = string.Empty;
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            var httpResponseMessage = await httpClient.PostAsJsonAsync("User/Authenticate", model);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                token = await httpResponseMessage.Content.ReadAsStringAsync();
                Response.Cookies.Append("access_token", token, new CookieOptions()
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true
                });
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Login");


        }

    }
}