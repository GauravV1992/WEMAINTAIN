using BusinessEntities.Common;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using WEMAINTAIN.Models;

namespace WEMAINTAIN.Areas.Client.Components
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public FooterViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
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
                    ViewBag.Logo = banner.Data.Where(i => i.Rank == 1 && i.BannerType == "Logo").Take(1).FirstOrDefault()?.Id + banner.Data.Where(i => i.Rank == 1).Take(1).FirstOrDefault()?.Ext;
                }
            }
            return View();
        }
    }
}

