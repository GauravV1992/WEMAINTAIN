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
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _iCategoryService;
        public CategoryController(ICategoryService iCategoryService)
        {
            _iCategoryService = iCategoryService;
        }
        [CustomAuthorize("Admin")]
        [HttpPost] 
        [ActionName("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] CategoryRequest request)
        {
            var res = await _iCategoryService.GetAll(request);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }
        [CustomAuthorize("Admin")]
        [HttpGet("{Id}")]
        [ActionName("GetById")]
        public async Task<IActionResult> GetById(long Id)
        {
            var res = await _iCategoryService.GetById(Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }
        [CustomAuthorize("Admin")]
        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> Post([FromBody] CategoryRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            LoginResponse user = Common.GetSessionData(HttpContext);
            viewModel.CreatedBy = user.Id;
            var res = await _iCategoryService.Add(viewModel);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }
        [CustomAuthorize("Admin")]
        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromBody] CategoryRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            LoginResponse user = Common.GetSessionData(HttpContext);
            viewModel.ModifiedBy = user.Id;
            var res = await _iCategoryService.Update(viewModel);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }
        [CustomAuthorize("Admin")]
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete([FromBody] ValueRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            var res = await _iCategoryService.Delete(viewModel.Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }
        [CustomAuthorize("Admin")]
        [HttpGet]
        [ActionName("GetPackageNames")]
        public async Task<IActionResult> GetPackageNames()
        {
            var res = await _iCategoryService.GetPackages();
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }
        [AllowAnonymous]
        [HttpGet]
        [ActionName("GetPackageSection")]
        public async Task<IActionResult> GetPackageSection()
        {
            var res = await _iCategoryService.GetPackageSection();
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }
    }
}