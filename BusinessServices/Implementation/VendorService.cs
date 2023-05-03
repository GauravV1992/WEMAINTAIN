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
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _iVendorRepository;
        private IMapper _mapper;
        public VendorService(IVendorRepository repository, IMapper mapper)
        {
            _iVendorRepository = repository;
            _mapper = mapper;
        }
        public async Task<ResultDto<long>> Add(VendorRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _iVendorRepository.Add(viewModel);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }
        public async Task<ResultDto<long>> Update(VendorRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _iVendorRepository.Update(viewModel);
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
            var response = await _iVendorRepository.Delete(Id);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }

        public async Task<ResultDto<IEnumerable<VendorResponse>>> GetAll(VendorRequest request)
        {
            var res = new ResultDto<IEnumerable<VendorResponse>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iVendorRepository.GetAll(request);

            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                try
                {
                    res.Data = _mapper.Map<IEnumerable<Vendor>, IEnumerable<VendorResponse>>(response);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }

        public async Task<ResultDto<VendorResponse>> GetById(long Id)
        {
            var res = new ResultDto<VendorResponse>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iVendorRepository.GetById(Id);
            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                res.Data = _mapper.Map<Vendor, VendorResponse>(response);
            }
            return res;
        }
    }
}