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
using System.Security.AccessControl;
//using AutoMapper;

namespace BusinessServices.Implementation
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _iCouponRepository;
        private IMapper _mapper;
        public CouponService(ICouponRepository repository, IMapper mapper)
        {
            _iCouponRepository = repository;
            _mapper = mapper;
        }
        public async Task<ResultDto<long>> Add(CouponRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
          
            var response = await _iCouponRepository.Add(viewModel);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }
        public async Task<ResultDto<long>> Update(CouponRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _iCouponRepository.Update(viewModel);
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
            var response = await _iCouponRepository.Delete(Id);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }

        public async Task<ResultDto<IEnumerable<CouponResponse>>> GetAll(CouponRequest request)
        {
            var res = new ResultDto<IEnumerable<CouponResponse>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iCouponRepository.GetAll(request);

            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                try
                {
                    res.Data = _mapper.Map<IEnumerable<Coupon>, IEnumerable<CouponResponse>>(response);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }

        public async Task<ResultDto<CouponResponse>> GetById(long Id)
        {
            var res = new ResultDto<CouponResponse>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iCouponRepository.GetById(Id);
            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                res.Data = _mapper.Map<Coupon, CouponResponse>(response);
            }
            return res;
        }

        public async Task<ResultDto<IList<SelectListItem>>> GetUserNames()
        {
            var res = new ResultDto<IList<SelectListItem>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iCouponRepository.GetUserNames();

            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                try
                {
                    res.Data = response;
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