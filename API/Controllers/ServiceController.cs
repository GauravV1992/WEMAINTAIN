using API.Helpers;
using API.JWTMiddleware;
using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
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
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _iServiceService;
        public ServiceController(IServiceService iServiceService)
        {
            _iServiceService = iServiceService;
        }

        [HttpPost]
        [ActionName("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] ServiceRequest request)
        {
            var res = await _iServiceService.GetAll(request);
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
            var res = await _iServiceService.GetById(Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> Post([FromBody] ServiceRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            LoginResponse user = Common.GetSessionData(HttpContext);
            viewModel.CreatedBy = user.Id;
            var res = await _iServiceService.Add(viewModel);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromBody] ServiceRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            LoginResponse user = Common.GetSessionData(HttpContext);
            viewModel.ModifiedBy = user.Id;
            var res = await _iServiceService.Update(viewModel);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete([FromBody] ValueRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            var res = await _iServiceService.Delete(viewModel.Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }
        [HttpGet("{Id}")]
        [ActionName("GetServiceNames")]
        public async Task<IActionResult> GetServiceNames(long id)
        {
            var res = await _iServiceService.GetServiceNames(id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }

    }
}