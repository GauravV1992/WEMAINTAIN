using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BusinessServices.Interface
{
    public interface IVendorService
    {
        Task<ResultDto<long>> Add(VendorRequest viewModel);
        Task<ResultDto<long>> Update(VendorRequest viewModel);
        Task<ResultDto<long>> Delete(long Id);
        Task<ResultDto<IEnumerable<VendorResponse>>> GetAll(VendorRequest request);
        Task<ResultDto<VendorResponse>> GetById(long Id);
        Task<ResultDto<IList<SelectListItem>>> GetCountryNames();
        Task<ResultDto<IList<SelectListItem>>> GetStateNames();
    }
}
