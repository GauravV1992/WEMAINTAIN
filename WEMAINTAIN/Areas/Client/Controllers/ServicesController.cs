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
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View("~/areas/client/views/Services.cshtml");
        }

    }
}