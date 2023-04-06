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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _iCategoryRepository;
        private IMapper _mapper;
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _iCategoryRepository = repository;
            _mapper = mapper;
        }
        //public ResultDto<long> Add(CategoryRequest viewModel)
        //{
        //    var res = new ResultDto<long>()
        //    {
        //        IsSuccess = false,
        //        Data = 0,
        //        Errors = new List<string>()
        //    };
        //    var response = _iCategoryRepository.Add(viewModel);
        //    if (response == -1)
        //    {
        //        res.Errors.Add("Color Name Already Exists !!");
        //    }
        //    else if (response == -2)
        //    {
        //        res.Errors.Add("Color Name length not greater than 5 char long !! !!");
        //    }
        //    else
        //    {
        //        res.IsSuccess = true;
        //        res.Data = response;
        //    }
        //    return res;
        //}
        //public ResultDto<long> Update(CategoryRequest viewModel)
        //{
        //    var res = new ResultDto<long>()
        //    {
        //        IsSuccess = false,
        //        Data = 0,
        //        Errors = new List<string>()
        //    };
        //    var response = _iCategoryRepository.Update(viewModel);
        //    if (response == -1)
        //    {
        //        res.Errors.Add("Color Name Already Exists !!");
        //    }
        //    else if (response == -2)
        //    {
        //        res.Errors.Add("Color Name length not greater than 5 char long !! !!");
        //    }
        //    else
        //    {
        //        res.IsSuccess = true;
        //        res.Data = response;
        //    }
        //    return res;
        //}
        //public ResultDto<long> Delete(long Id)
        //{
        //    var res = new ResultDto<long>()
        //    {
        //        IsSuccess = false,
        //        Data = 0,
        //        Errors = new List<string>()
        //    };
        //    var response = _iCategoryRepository.Delete(Id);
        //    if (response == -1)
        //    {
        //        res.Errors.Add("Color Does Not Exists For This Id!!");
        //    }
        //    else
        //    {
        //        res.IsSuccess = true;
        //        res.Data = response;
        //    }
        //    return res;
        //}

        public async Task<ResultDto<IEnumerable<CategoryResponse>>> GetAll()
        {
            var res = new ResultDto<IEnumerable<CategoryResponse>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iCategoryRepository.GetAll();

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
                catch (Exception ex) {
                    throw ex; 
                }
            }
            return res;
        }

        //public ResultDto<CategoryResponse> GetById(long Id)
        //{
        //    var res = new ResultDto<CategoryResponse>()
        //    {
        //        IsSuccess = false,
        //        Data = null,
        //        Errors = new List<string>()
        //    };

        //    var response = _iCategoryRepository.GetById(Id);
        //    if (response == null)
        //    {
        //        res.Errors.Add("Data Not Found !!");
        //    }
        //    else
        //    {
        //        res.IsSuccess = true;
        //        res.Data = _mapper.Map<Package, CategoryResponse>(response);
        //    }

        //    return res;
        //}
    }
}