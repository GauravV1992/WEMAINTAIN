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
    public class SubPackageService : ISubPackageService
    {
        private readonly ISubPackageRepository _iSubPackageRepository;
        private IMapper _mapper;
        public SubPackageService(ISubPackageRepository repository, IMapper mapper)
        {
            _iSubPackageRepository = repository;
            _mapper = mapper;
        }
        public async Task<ResultDto<long>> Add(SubPackageRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _iSubPackageRepository.Add(viewModel);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }
        public async Task<ResultDto<long>> Update(SubPackageRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _iSubPackageRepository.Update(viewModel);
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
            var response = await _iSubPackageRepository.Delete(Id);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }

        public async Task<ResultDto<IEnumerable<SubPackageResponse>>> GetAll(SubPackageRequest request)
        {
            var res = new ResultDto<IEnumerable<SubPackageResponse>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iSubPackageRepository.GetAll(request);

            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                try
                {
                    res.Data = _mapper.Map<IEnumerable<SubPackage>, IEnumerable<SubPackageResponse>>(response);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }

        public async Task<ResultDto<SubPackageResponse>> GetById(long Id)
        {
            var res = new ResultDto<SubPackageResponse>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iSubPackageRepository.GetById(Id);
            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                res.Data = _mapper.Map<SubPackage, SubPackageResponse>(response);
            }
            return res;
        }
        public async Task<ResultDto<IList<SelectListItem>>> GetSubPackageNames(long id)
        {
            var res = new ResultDto<IList<SelectListItem>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iSubPackageRepository.GetSubPackageNames(id);

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