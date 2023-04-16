using BusinessEntities.RequestDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Repositories.Interface
{
    public interface IServiceRepository
    {
        Task<long> Add(ServiceRequest viewModel);
        Task<long> Update(ServiceRequest viewModel);
        Task<long> Delete(long Id);
        Task<IEnumerable<Service>> GetAll(ServiceRequest request);
        Task<Service> GetById(long Id);

        Task<IList<SelectListItem>> GetServiceNames(long id);
    }
}
