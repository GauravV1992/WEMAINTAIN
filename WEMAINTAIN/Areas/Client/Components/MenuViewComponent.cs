using BusinessEntities.Common;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using WEMAINTAIN.Models;

namespace WEMAINTAIN.Areas.Client.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public MenuViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = new UserResponse();
            var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
            httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Authorization, "Bearer " + Common.GetAccessToken(HttpContext) + "");
            var httpResponseMessage = await httpClient.GetAsync("User/GetUserDetails");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                user = JsonSerializer.Deserialize<UserResponse>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            var banner = new ResultDto<IEnumerable<BannerResponse>>();
            var bannerResponse = await httpClient.GetAsync("Banner/GetBanner");
            if (bannerResponse.IsSuccessStatusCode)
            {
                var contentStreamBanner = await bannerResponse.Content.ReadAsStringAsync();
                banner = JsonSerializer.Deserialize<ResultDto<IEnumerable<BannerResponse>>>(contentStreamBanner, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (banner?.Data != null)
                {
                    ViewBag.Logo = banner.Data.Where(i => i.Rank == 1 && i.BannerType == "Logo").Take(1).FirstOrDefault()?.Id + banner.Data.Where(i => i.Rank == 1).Take(1).FirstOrDefault()?.Ext;
                }
            }
            return View(user);
        }
    }
}

