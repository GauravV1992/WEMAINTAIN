using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using BusinessServices.Interface;
using Repositories.Interface;
using AutoMapper.Mappers;
using Repositories;
using BusinessEntities.Common;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
//using AutoMapper;

namespace BusinessServices.Implementation
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _iBannerRepository;
        private IMapper _mapper;
        public BannerService(IBannerRepository repository, IMapper mapper)
        {
            _iBannerRepository = repository;
            _mapper = mapper;
        }
        public async Task<ResultDto<long>> Add(BannerRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _iBannerRepository.Add(viewModel);
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
            var response = await _iBannerRepository.Delete(Id);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }

        public async Task<ResultDto<IEnumerable<BannerResponse>>> GetAll(BannerRequest request)
        {
            var res = new ResultDto<IEnumerable<BannerResponse>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iBannerRepository.GetAll(request);

            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                try
                {
                    res.Data = _mapper.Map<IEnumerable<Banner>, IEnumerable<BannerResponse>>(response);

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