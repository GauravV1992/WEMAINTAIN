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
//using AutoMapper;

namespace BusinessServices.Implementation
{
    public class PackageRateLogService : IPackageRateLogService
    {
        private readonly IPackageRateLogRepository _iPackageRateLogRepository;
        private IMapper _mapper;
        public PackageRateLogService(IPackageRateLogRepository repository, IMapper mapper)
        {
            _iPackageRateLogRepository = repository;
            _mapper = mapper;
        }
        
        public async Task<ResultDto<IEnumerable<PackageRateLogResponse>>> GetAll(PackageRateLogRequest request)
        {
            var res = new ResultDto<IEnumerable<PackageRateLogResponse>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iPackageRateLogRepository.GetAll(request);

            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                try
                {
                    res.Data = _mapper.Map<IEnumerable<PackageRateLog>, IEnumerable<PackageRateLogResponse>>(response);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }
    }
}