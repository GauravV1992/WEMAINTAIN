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
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _iServiceRepository;
        private IMapper _mapper;
        public ServiceService(IServiceRepository repository, IMapper mapper)
        {
            _iServiceRepository = repository;
            _mapper = mapper;
        }
        public async Task<ResultDto<long>> Add(ServiceRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _iServiceRepository.Add(viewModel);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }
        public async Task<ResultDto<long>> Update(ServiceRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _iServiceRepository.Update(viewModel);
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
            var response = await _iServiceRepository.Delete(Id);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }

        public async Task<ResultDto<IEnumerable<ServiceResponse>>> GetAll()
        {
            var res = new ResultDto<IEnumerable<ServiceResponse>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iServiceRepository.GetAll();

            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                try
                {
                    res.Data = _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceResponse>>(response);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }

        public async Task<ResultDto<ServiceResponse>> GetById(long Id)
        {
            var res = new ResultDto<ServiceResponse>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iServiceRepository.GetById(Id);
            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                res.Data = _mapper.Map<Service, ServiceResponse>(response);
            }
            return res;
        }
    }
}