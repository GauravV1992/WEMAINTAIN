using API.Helpers;
using API.JWTMiddleware;
using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using BusinessServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Repositories;
using Repositories.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;


namespace API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IUserService _iUserService;
        private readonly IJWTAuthenticaitonManagerService _iJWTAuthenticaitonManagerService;
        public UserController(IUserService iUserService, IJWTAuthenticaitonManagerService iJWTAuthenticaitonManagerService, IOptions<AppSettings> appSettings)
        {
            _iJWTAuthenticaitonManagerService = iJWTAuthenticaitonManagerService;
            _iUserService = iUserService;
            _appSettings = appSettings.Value;
        }
        [AllowAnonymous]
        [HttpPost]
        [ActionName("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest user)
        {
            var token = await _iJWTAuthenticaitonManagerService.Authentiate(user.PhoneNumber, user.Credential);
            if (token == null)
            {
                return Unauthorized();
            }
            else
            {
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescripter = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, token.FirstName + " " + token.LastName),
                        new Claim("Id", token.Id.ToString()),
                        new Claim("Email", token.Email),
                        new Claim("MobileNo", token.MobileNo),
                        //new Claim("Address", token.Address),
                        //new Claim("Username", token.Username)
                    }),
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var res = tokenHandler.CreateToken(tokenDescripter);
                return Ok(tokenHandler.WriteToken(res));
            }
        }

        [CustomAuthorize("Admin")]
        [HttpPost]
        [ActionName("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] UserRequest request)
        {
            // LoginResponse user = Common.GetUserSessionData(HttpContext);
            var res = await _iUserService.GetAll(request);
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
            var res = await _iUserService.GetById(Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return NotFound(res);
        }
        [AllowAnonymous]
        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> Post([FromBody] UserRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }

            var res = await _iUserService.Add(viewModel);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromBody] UserRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            LoginResponse user = Common.GetSessionData(HttpContext);
            viewModel.ModifiedBy = user.Id;
            var res = await _iUserService.Update(viewModel);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }
        [HttpPost]
        
        [ActionName("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            LoginResponse user = Common.GetSessionData(HttpContext);
            viewModel.ModifiedBy = user.Id;
            var res = await _iUserService.UpdateProfile(viewModel);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }

        [ActionName("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UserRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            LoginResponse user = Common.GetSessionData(HttpContext);
            viewModel.ModifiedBy = user.Id;
            var res = await _iUserService.UpdatePassword(viewModel);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }


        [HttpPost]
        [CustomAuthorize("Admin")]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete([FromBody] ValueRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            var res = await _iUserService.Delete(viewModel.Id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }


        //[HttpPost]
        //[ActionName("CheckUserLogin")]
        //public async Task<IActionResult> CheckUserLogin([FromBody] UserRequest viewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState.Values.ToArray());
        //    }
        //    var res = await _iUserService.CheckUserLogin(viewModel);
        //    if (res.IsSuccess)
        //    {
        //        return Ok(res);
        //    }

        //    return NotFound(res);
        //}

        [HttpPost]
        [ActionName("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] UserRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToArray());
            }
            var res = await _iUserService.ForgetPassword(viewModel);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return NotFound(res);
        }
        [HttpGet]
        [CustomAuthorize("Admin")]
        [ActionName("GetUserDetails")]
        public IActionResult GetUserDetails()
        {
            LoginResponse user = Common.GetSessionData(HttpContext);
            if (user != null)
                return Ok(user);
            else return NotFound();
        }

    }
}