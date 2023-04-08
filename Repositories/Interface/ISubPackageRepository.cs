using BusinessEntities.RequestDto;

namespace Repositories.Interface
{
    public interface ISubPackageRepository
    {
        Task<long> Add(SubPackageRequest viewModel);
        Task<long> Update(SubPackageRequest viewModel);
        Task<long> Delete(long Id);
        Task<IEnumerable<SubPackage>> GetAll();
        Task<SubPackage> GetById(long Id);
    }
}
