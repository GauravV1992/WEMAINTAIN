using BusinessEntities.RequestDto;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<long> Add(CategoryRequest viewModel);
        Task<long> Update(CategoryRequest viewModel);
        Task<long> Delete(long Id);
        Task<IEnumerable<Package>> GetAll();
        Task<Package> GetById(long Id);
        Task<IList<SelectListItem>> GetPackages();
    }
}
