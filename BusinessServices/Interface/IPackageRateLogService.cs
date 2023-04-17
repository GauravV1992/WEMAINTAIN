using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BusinessServices.Interface
{
    public interface IPackageRateLogService
    {
        Task<ResultDto<IEnumerable<PackageRateLogResponse>>> GetAll(PackageRateLogRequest request);
    }
}
