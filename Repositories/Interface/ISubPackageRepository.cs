using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Repositories.Interface
{
    public interface ISubPackageRepository
    {
        Task<long> Add(SubPackageRequest viewModel);
        Task<long> Update(SubPackageRequest viewModel);
        Task<long> Delete(long Id);
        Task<IEnumerable<SubPackage>> GetAll(SubPackageRequest request);
        Task<SubPackage> GetById(long Id);
        Task<IList<SelectListItem>> GetSubPackageNames(long id);
        Task<IEnumerable<SubPackage>> GetSubPackageSection(long id);
    }
}
