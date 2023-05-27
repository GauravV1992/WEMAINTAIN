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
    public class AboutUsController : Controller
    {
        private readonly ILogger<AboutUsController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutUsController(ILogger<AboutUsController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            var banner = new ResultDto<IEnumerable<BannerResponse>>();
            var bannerResponse = await httpClient.GetAsync("Banner/GetBanner");
            if (bannerResponse.IsSuccessStatusCode)
            {
                var contentStreamBanner = await bannerResponse.Content.ReadAsStringAsync();
                banner = JsonSerializer.Deserialize<ResultDto<IEnumerable<BannerResponse>>>(contentStreamBanner, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                //(banner.Data.Where(i => i.BannerType == "AboutUs") && ((banner.Data.Where(i => i.Rank == 1).Take(1).FirstOrDefault()?.Id + banner.Data.Where(i => i.Rank == 1).Take(1).FirstOrDefault()?.Ext)));
                if (banner?.Data != null)
                    ViewBag.AboutUs =banner.Data.Where(i => i.Rank == 1 && i.BannerType=="AboutUs").Take(1).FirstOrDefault()?.Id + banner.Data.Where(i => i.Rank == 1).Take(1).FirstOrDefault()?.Ext;
            }
            return View("~/Areas/Client/Views/AboutUs.cshtml");

        }

    }
}