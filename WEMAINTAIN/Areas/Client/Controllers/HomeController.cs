using BusinessEntities.Common;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using WEMAINTAIN.Models;
using BusinessEntities.RequestDto;
using Microsoft.AspNetCore.Authorization;

namespace WEMAINTAIN.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            var banner = new ResultDto<IEnumerable<BannerResponse>>();
            var bannerResponse = await httpClient.GetAsync("Banner/GetBanner");
            if (bannerResponse.IsSuccessStatusCode)
            {
                var contentStreamBanner = await bannerResponse.Content.ReadAsStringAsync();
                banner = JsonSerializer.Deserialize<ResultDto<IEnumerable<BannerResponse>>>(contentStreamBanner, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (banner?.Data != null)
                {
                    ViewBag.BannerImg = banner.Data.Where(i => i.Rank == 1 && i.BannerType == "HomePage").Take(1).FirstOrDefault()?.Id + banner.Data.Where(i => i.Rank == 1).Take(1).FirstOrDefault()?.Ext;
                    ViewBag.Logo = banner.Data.Where(i => i.Rank == 1 && i.BannerType == "Logo").Take(1).FirstOrDefault()?.Id + banner.Data.Where(i => i.Rank == 1).Take(1).FirstOrDefault()?.Ext;
                }
                    
            }
            return View("~/areas/client/views/home.cshtml");
        }

        public async Task<IActionResult> GetCategorySection()
        {
            var categories = new ResultDto<IEnumerable<CategoryResponse>>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            var httpResponseMessage = await httpClient.GetAsync("Category/GetPackageSection");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                categories = JsonSerializer.Deserialize<ResultDto<IEnumerable<CategoryResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return PartialView("~/areas/client/views/_category.cshtml", categories?.Data.ToList());
        }
        public async Task<IActionResult> GetSubPackageSection(int packageId)
        {
            var categories = new ResultDto<IEnumerable<SubPackageResponse>>();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            var httpResponseMessage = await httpClient.GetAsync("SubPackage/GetSubPackageSection/" + packageId + "");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                categories = JsonSerializer.Deserialize<ResultDto<IEnumerable<SubPackageResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return PartialView("~/areas/client/views/_subpackage.cshtml", categories?.Data.ToList());
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
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
                    SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                    Secure = true
                });
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

    }
}