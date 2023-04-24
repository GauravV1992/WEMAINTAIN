using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WEMAINTAIN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminController(ILogger<AdminController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpGet("Logout")]
        public ActionResult Logout()
        {
            Response.Cookies.Delete("access_token", new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true
            });
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
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
            return View(model);


        }

    }
}