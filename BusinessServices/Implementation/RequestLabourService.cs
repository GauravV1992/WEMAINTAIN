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
    public class RequestLabourService : IRequestLabourService
    {
        private readonly IRequestLabourRepository _IRequestLabourRepository;
        private IMapper _mapper;
        public RequestLabourService(IRequestLabourRepository repository, IMapper mapper)
        {
            _IRequestLabourRepository = repository;
            _mapper = mapper;
        }
        public async Task<ResultDto<IEnumerable<RequestLabourResponse>>> GetAll(RequestLabourRequest request)
        {
            var res = new ResultDto<IEnumerable<RequestLabourResponse>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _IRequestLabourRepository.GetAll(request);

            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                try
                {
                    res.Data = _mapper.Map<IEnumerable<RequestLabour>, IEnumerable<RequestLabourResponse>>(response);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }

        public async Task<ResultDto<RequestLabourResponse>> GetById(long Id)
        {
            var res = new ResultDto<RequestLabourResponse>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _IRequestLabourRepository.GetById(Id);
            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                res.Data = _mapper.Map<RequestLabour, RequestLabourResponse>(response);
            }
            return res;
        }

        
    }
}