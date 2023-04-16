using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;

namespace BusinessServices.Interface
{
    public interface IPackageRateService
    {
        Task<ResultDto<long>> Add(PackageRateRequest viewModel);
        Task<ResultDto<long>> Update(PackageRateRequest viewModel);
        Task<ResultDto<long>> Delete(long Id);
        Task<ResultDto<IEnumerable<PackageRateResponse>>> GetAll(PackageRateRequest request);
        Task<ResultDto<PackageRateResponse>> GetById(long Id);
    }
}
