using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusinessServices.Interface
{
    public interface ISubPackageService
    {
        Task<ResultDto<long>> Add(SubPackageRequest viewModel);
        Task<ResultDto<long>> Update(SubPackageRequest viewModel);
        Task<ResultDto<long>> Delete(long Id);
        Task<ResultDto<IEnumerable<SubPackageResponse>>> GetAll(SubPackageRequest request);
        Task<ResultDto<SubPackageResponse>> GetById(long Id);

        Task<ResultDto<IList<SelectListItem>>> GetSubPackageNames(long Id);

        Task<ResultDto<IEnumerable<SubPackageResponse>>> GetSubPackageSection(long Id);
        Task<ResultDto<SubPackagePriceDetailsResponse>> GetSubPackagePriceDetails(long id, string amcPeriod);
    }
}
