using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BusinessServices.Interface
{
    public interface ICouponService
    {
        Task<ResultDto<long>> Add(CouponRequest viewModel);
        Task<ResultDto<long>> Update(CouponRequest viewModel);
        Task<ResultDto<long>> Delete(long Id);
        Task<ResultDto<IEnumerable<CouponResponse>>> GetAll(CouponRequest request);
        Task<ResultDto<CouponResponse>> GetById(long Id);

        Task<ResultDto<IList<SelectListItem>>> GetUserNames();
    }
}
