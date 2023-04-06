using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using WEMAINTAIN.Models;



namespace WEMAINTAIN.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryController(ILogger<CategoryController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View("Categories");
        }
        [HttpPost]
        public async Task<ActionResult> GetAll(CategoryRequest request)
        {
            try
            {
                //var page = request.Start / request.Length + 1;
                var categories = new ResultDto<IEnumerable<CategoryResponse>>();
                var httpClient = _httpClientFactory.CreateClient("WEMAINTAIN");
                var httpResponseMessage = await httpClient.GetAsync("Category/GetAll");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<ResultDto<IEnumerable<CategoryResponse>>>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    //categories = await JsonConvert.DeserializeObject<ResultDto<IEnumerable<CategoryResponse>>>(contentStream);
                }
                int totalRecords = categories != null && categories.IsSuccess ? categories.Data.FirstOrDefault().TotalRecords : 0;
                return Json(new
                {
                    recordsTotal = 1,
                    data = categories.Data.ToList()
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}