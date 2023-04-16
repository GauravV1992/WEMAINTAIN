using API.JWTMiddleware;
using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessServices.Implementation;
using BusinessServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace API.Controllers
{
    [CustomAuthorize("Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PackageRateLogController : ControllerBase
    {
        private readonly IPackageRateLogService _iPackageRateLogService;
        public PackageRateLogController(IPackageRateLogService iPackageRateLogService)
        {
            _iPackageRateLogService = iPackageRateLogService;
        }

        [HttpPost] 
        [ActionName("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] PackageRateLogRequest request)
        {
            var res = await _iPackageRateLogService.GetAll(request);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }
    }
}