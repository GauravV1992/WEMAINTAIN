using BusinessEntities.RequestDto;

namespace Repositories.Interface
{
    public interface IPackageRateRepository
    {
        Task<long> Add(PackageRateRequest viewModel);
        Task<long> Update(PackageRateRequest viewModel);
        Task<long> Delete(long Id);
        Task<IEnumerable<PackageRate>> GetAll();
        Task<PackageRate> GetById(long Id);
    }
}
