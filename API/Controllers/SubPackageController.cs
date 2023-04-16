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
    public class SubPackageController : ControllerBase
    {
        private readonly ISubPackageService _iSubPackageService;
        public SubPackageController(ISubPackageService iSubPackageService)
        {
            _iSubPackageService = iSubPackageService;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _iSubPackageService.GetAll();
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
            var res = await _iSubPackageService.GetById(Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> Post([FromBody] SubPackageRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            LoginResponse user = Common.GetSessionData(HttpContext);
            viewModel.CreatedBy = user.Id;
            var res = await _iSubPackageService.Add(viewModel);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromBody] SubPackageRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            LoginResponse user = Common.GetSessionData(HttpContext);
            viewModel.ModifiedBy = user.Id;
            var res = await _iSubPackageService.Update(viewModel);
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
            var res = await _iSubPackageService.Delete(viewModel.Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }
        [HttpGet("{Id}")]
        [ActionName("GetSubPackageNames")]
        public async Task<IActionResult> GetSubPackageNames(long Id)
        {
            var res = await _iSubPackageService.GetSubPackageNames(Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }

    }
}