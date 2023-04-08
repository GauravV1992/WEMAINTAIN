using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace API.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _iServiceService;
        public ServiceController(IServiceService iServiceService)
        {
            _iServiceService = iServiceService;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _iServiceService.GetAll();
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

    }
}