using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using BusinessServices.Interface;
using Repositories.Interface;
using AutoMapper.Mappers;
using Repositories;
using BusinessEntities.Common;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.Implementation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Azure;

namespace BusinessServices.Implementation
{
    public class JWTAuthenticaitonManagerService : IJWTAuthenticaitonManagerService
    {
        private readonly IJWTAuthenticaitonManagerRepository _iJWTAuthenticaitonManagerRepository;
        private IMapper _mapper;

        public JWTAuthenticaitonManagerService(IJWTAuthenticaitonManagerRepository repository, IMapper mapper)
        {
            _iJWTAuthenticaitonManagerRepository = repository;
            _mapper = mapper;

        }
        public async Task<LoginResponse> Authentiate(string usename, string password)
        {
            var user = await _iJWTAuthenticaitonManagerRepository.Authentiate(usename, password);
            if (user != null && (user.Username == usename && user.Password == password))
            {
                return _mapper.Map<User, LoginResponse>(user);
            }
            else
            {
                return null;
            }
        }
    }


}