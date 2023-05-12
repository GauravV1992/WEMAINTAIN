using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Repositories.Interface
{
    public interface IProductRepository
    {
        Task<long> Add(ProductRequest viewModel);
        Task<long> Update(ProductRequest viewModel);
        Task<long> Delete(long Id);
        Task<IEnumerable<Product>> GetAll(ProductRequest request);
        Task<Product> GetById(long Id);
        Task<IList<SelectListItem>> GetVendorName();
    }
}
