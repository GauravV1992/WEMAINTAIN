using API.Helpers;
using API.JWTMiddleware;
using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using BusinessServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace API.Controllers
{
    [CustomAuthorize("Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RequestLabourController : ControllerBase
    {
        private readonly IRequestLabourService _iRequestLabourService;
        public RequestLabourController(IRequestLabourService iRequestLabourService)
        {
            _iRequestLabourService = iRequestLabourService;
        }

        [HttpPost] 
        [ActionName("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] RequestLabourRequest request)
        {
            var res = await _iRequestLabourService.GetAll(request);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }

        [HttpGet("{Id}")]
        [ActionName("GetById")]
        public async Task<IActionResult> GetById(long Id)
        {
            var res = await _iRequestLabourService.GetById(Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }
    }
}