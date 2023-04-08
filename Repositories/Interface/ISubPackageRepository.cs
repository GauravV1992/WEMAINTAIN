using BusinessEntities.RequestDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Repositories.Interface
{
    public interface ISubPackageRepository
    {
        Task<long> Add(SubPackageRequest viewModel);
        Task<long> Update(SubPackageRequest viewModel);
        Task<long> Delete(long Id);
        Task<IEnumerable<SubPackage>> GetAll();
        Task<SubPackage> GetById(long Id);
        Task<IList<SelectListItem>> GetSubPackageNames(long id);
    }
}
