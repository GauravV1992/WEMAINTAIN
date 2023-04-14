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
    public class PurchaseDetailsController : ControllerBase
    {
        private readonly IPurchaseDetailsService _iPurchaseDetailsService;
        public PurchaseDetailsController(IPurchaseDetailsService iPurchaseDetailsService)
        {
            _iPurchaseDetailsService = iPurchaseDetailsService;
        }

        [HttpGet] 
        [ActionName("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _iPurchaseDetailsService.GetAll();
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
            var res = await _iPurchaseDetailsService.GetById(Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> Post([FromBody] PurchaseDetailsRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            var res = await _iPurchaseDetailsService.Add(viewModel);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }
    }
}