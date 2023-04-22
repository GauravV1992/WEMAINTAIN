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
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _iCouponService;
        public CouponController(ICouponService iCouponService)
        {
            _iCouponService = iCouponService;
        }

        [HttpPost] 
        [ActionName("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] CouponRequest request)
        {
            var res = await _iCouponService.GetAll(request);
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
            var res = await _iCouponService.GetById(Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> Post([FromBody] CouponRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            LoginResponse user = Common.GetSessionData(HttpContext);
            viewModel.CreatedBy = user.Id;
            var res = await _iCouponService.Add(viewModel);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromBody] CouponRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            LoginResponse user = Common.GetSessionData(HttpContext);
            viewModel.ModifiedBy = user.Id;
            var res = await _iCouponService.Update(viewModel);
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
            var res = await _iCouponService.Delete(viewModel.Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }

        [HttpGet]
        [ActionName("GetUserNames")]
        public async Task<IActionResult> GetUserNames()
        {
            var res = await _iCouponService.GetUserNames();
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }

    }
}