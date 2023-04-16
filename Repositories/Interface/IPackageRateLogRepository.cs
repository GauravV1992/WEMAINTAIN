using BusinessEntities.RequestDto;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Repositories.Interface
{
    public interface IPackageRateLogRepository
    {
        Task<IEnumerable<PackageRateLog>> GetAll(PackageRateLogRequest request);
    }
}
