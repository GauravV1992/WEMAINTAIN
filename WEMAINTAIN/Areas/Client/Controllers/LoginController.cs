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
            return RedirectToAction("Index", "Home");
        }

        

    }
}