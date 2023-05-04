using BusinessEntities.RequestDto;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Repositories.Interface
{
    public interface IVendorRepository
    {
        Task<long> Add(VendorRequest viewModel);
        Task<long> Update(VendorRequest viewModel);
        Task<long> Delete(long Id);
        Task<IEnumerable<Vendor>> GetAll(VendorRequest request);
        Task<Vendor> GetById(long Id);
        Task<IList<SelectListItem>> GetCountryNames();
        Task<IList<SelectListItem>> GetStateNames();
    }
}
