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
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _iCountryRepository;
        private IMapper _mapper;
        public CountryService(ICountryRepository repository, IMapper mapper)
        {
            _iCountryRepository = repository;
            _mapper = mapper;
        }
        public async Task<ResultDto<IList<SelectListItem>>> GetCountryNames()
        {
            var res = new ResultDto<IList<SelectListItem>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _iCountryRepository.GetCountryNames();

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