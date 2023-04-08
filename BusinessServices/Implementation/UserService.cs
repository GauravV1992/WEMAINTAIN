using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using BusinessServices.Interface;
using Repositories.Interface;
using AutoMapper.Mappers;
using Repositories;
using BusinessEntities.Common;
using AutoMapper;
//using AutoMapper;

namespace BusinessServices.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _IUserRepository;
        private IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _IUserRepository = repository;
            _mapper = mapper;
        }
        public async Task<ResultDto<long>> Add(UserRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _IUserRepository.Add(viewModel);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }
        public async Task<ResultDto<long>> Update(UserRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _IUserRepository.Update(viewModel);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }
        public async Task<ResultDto<long>> Delete(long Id)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _IUserRepository.Delete(Id);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }

        public async Task<ResultDto<IEnumerable<UserResponse>>> GetAll()
        {
            var res = new ResultDto<IEnumerable<UserResponse>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _IUserRepository.GetAll();

            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                try
                {
                    res.Data = _mapper.Map<IEnumerable<User>, IEnumerable<UserResponse>>(response);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }

        public async Task<ResultDto<UserResponse>> GetById(long Id)
        {
            var res = new ResultDto<UserResponse>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _IUserRepository.GetById(Id);
            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                res.Data = _mapper.Map<User, UserResponse>(response);
            }
            return res;
        }
    }
}