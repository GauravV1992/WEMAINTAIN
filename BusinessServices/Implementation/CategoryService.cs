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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _iCategoryRepository;
        private IMapper _mapper;
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _iCategoryRepository = repository;
            _mapper = mapper;
        }
        public async Task<ResultDto<long>> Add(CategoryRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _iCategoryRepository.Add(viewModel);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }
        public async Task<ResultDto<long>> Update(CategoryRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _iCategoryRepository.Update(viewModel);
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
            var response = await _iCategoryRepository.Delete(Id);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }

        public async Task<ResultDto<IEnumerable<CategoryResponse>>> GetAll(CategoryRequest request)
        {
            var res = new ResultDto<IEnumerable<CategoryResponse>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iCategoryRepository.GetAll(request);

            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                try
                {
                    res.Data = _mapper.Map<IEnumerable<Package>, IEnumerable<CategoryResponse>>(response);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }

        public async Task<ResultDto<CategoryResponse>> GetById(long Id)
        {
            var res = new ResultDto<CategoryResponse>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iCategoryRepository.GetById(Id);
            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                res.Data = _mapper.Map<Package, CategoryResponse>(response);
            }
            return res;
        }

        public async Task<ResultDto<IList<SelectListItem>>> GetPackages()
        {
            var res = new ResultDto<IList<SelectListItem>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iCategoryRepository.GetPackages();

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