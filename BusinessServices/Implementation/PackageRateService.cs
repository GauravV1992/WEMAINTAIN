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
    public class PackageRateService : IPackageRateService
    {
        private readonly IPackageRateRepository _IPackageRateRepository;
        private IMapper _mapper;
        public PackageRateService(IPackageRateRepository repository, IMapper mapper)
        {
            _IPackageRateRepository = repository;
            _mapper = mapper;
        }
        public async Task<ResultDto<long>> Add(PackageRateRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _IPackageRateRepository.Add(viewModel);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }
        public async Task<ResultDto<long>> Update(PackageRateRequest viewModel)
        {
            var res = new ResultDto<long>()
            {
                IsSuccess = false,
                Data = 0,
                Errors = new List<string>()
            };
            var response = await _IPackageRateRepository.Update(viewModel);
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
            var response = await _IPackageRateRepository.Delete(Id);
            res.IsSuccess = true;
            res.Data = response;
            return res;
        }

        public async Task<ResultDto<IEnumerable<PackageRateResponse>>> GetAll(PackageRateRequest request)
        {
            var res = new ResultDto<IEnumerable<PackageRateResponse>>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _IPackageRateRepository.GetAll(request);

            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                try
                {
                    res.Data = _mapper.Map<IEnumerable<PackageRate>, IEnumerable<PackageRateResponse>>(response);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }

        //public Task<ResultDto<IEnumerable<PackageRateResponse>>> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ResultDto<PackageRateResponse>> GetById(long Id)
        {
            var res = new ResultDto<PackageRateResponse>()
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>()
            };

            var response = await _IPackageRateRepository.GetById(Id);
            if (response == null)
            {
                res.Errors.Add("Data Not Found !!");
            }
            else
            {
                res.IsSuccess = true;
                res.Data = _mapper.Map<PackageRate, PackageRateResponse>(response);
            }
            return res;
        }

        
    }
}