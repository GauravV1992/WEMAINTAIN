using BusinessEntities.RequestDto;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Repositories.Interface
{
    public interface IBannerRepository
    {
        Task<long> Add(BannerRequest viewModel);
        Task<long> Delete(long Id);
        Task<IEnumerable<Banner>> GetAll(BannerRequest request);
        
    }
}
