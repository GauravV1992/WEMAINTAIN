using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BusinessServices.Interface
{
    public interface IProductService
    {
        Task<ResultDto<long>> Add(ProductRequest viewModel);
        Task<ResultDto<long>> Update(ProductRequest viewModel);
        Task<ResultDto<long>> Delete(long Id);
        Task<ResultDto<IEnumerable<ProductResponse>>> GetAll(ProductRequest request);
        Task<ResultDto<ProductResponse>> GetById(long Id);
        Task<ResultDto<IList<SelectListItem>>> GetVendorName();
    }
}
