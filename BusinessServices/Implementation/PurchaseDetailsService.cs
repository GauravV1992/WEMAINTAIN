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
    public class PurchaseDetailsService : IPurchaseDetailsService
    {
        private readonly IPurchaseDetailsRepository _IPurchaseDetailsRepository;
        private IMapper _mapper;
        public PurchaseDetailsService(IPurchaseDetailsRepository repository, IMapper mapper)
        {
            _IPurchaseDetailsRepository = repository;
            _mapper = mapper;
        }
        public async Task<ResultDto<long>> Add(PurchaseDetailsRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _IPurchaseDetailsRepository.Add(viewModel);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }

        public async Task<ResultDto<IEnumerable<PurchaseDetailsResponse>>> GetAll()
        {
            var res = new ResultDto<IEnumerable<PurchaseDetailsResponse>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _IPurchaseDetailsRepository.GetAll();

            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                try
                {
                    res.Data = _mapper.Map<IEnumerable<PurchaseDetails>, IEnumerable<PurchaseDetailsResponse>>(response);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }

        public async Task<ResultDto<PurchaseDetailsResponse>> GetById(long Id)
        {
            var res = new ResultDto<PurchaseDetailsResponse>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _IPurchaseDetailsRepository.GetById(Id);
            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                res.Data = _mapper.Map<PurchaseDetails, PurchaseDetailsResponse>(response);
            }
            return res;
        }
    }
}