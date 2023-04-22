using BusinessEntities.RequestDto;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Repositories.Interface
{
    public interface ICouponRepository
    {
        Task<long> Add(CouponRequest viewModel);
        Task<long> Update(CouponRequest viewModel);
        Task<long> Delete(long Id);
        Task<IEnumerable<Coupon>> GetAll(CouponRequest request);
        Task<Coupon> GetById(long Id);
        Task<IList<SelectListItem>> GetUserNames();
    }
}
