using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;

namespace BusinessServices.Interface
{
    public interface IPurchaseDetailsService
    {
        Task<ResultDto<long>> Add(PurchaseDetailsRequest viewModel);
        Task<ResultDto<PurchaseDetailsWithServicesResponse>> GetAll(PurchaseDetailsRequest request);
        Task<ResultDto<PurchaseDetailsResponse>> GetById(long Id);
        Task<ResultDto<IEnumerable<PurchaseServicesResponse>>> PurchaseServicesById(long Id);
    }
}
