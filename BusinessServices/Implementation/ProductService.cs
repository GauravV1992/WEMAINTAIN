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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _iProductRepository;
        private IMapper _mapper;
        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _iProductRepository = repository;
            _mapper = mapper;
        }
        public async Task<ResultDto<long>> Add(ProductRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _iProductRepository.Add(viewModel);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }
        public async Task<ResultDto<long>> Update(ProductRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _iProductRepository.Update(viewModel);
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
            var response = await _iProductRepository.Delete(Id);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }

        public async Task<ResultDto<IEnumerable<ProductResponse>>> GetAll(ProductRequest request)
        {
            var res = new ResultDto<IEnumerable<ProductResponse>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iProductRepository.GetAll(request);

            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                try
                {
                    res.Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponse>>(response);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }

        public async Task<ResultDto<ProductResponse>> GetById(long Id)
        {
            var res = new ResultDto<ProductResponse>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iProductRepository.GetById(Id);
            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                res.Data = _mapper.Map<Product, ProductResponse>(response);
            }
            return res;
        }

        public async Task<ResultDto<IList<SelectListItem>>> GetVendorName()
        {
            var res = new ResultDto<IList<SelectListItem>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iProductRepository.GetVendorName();

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