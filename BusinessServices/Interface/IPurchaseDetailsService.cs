using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;

namespace BusinessServices.Interface
{
    public interface IPurchaseDetailsService
    {
        Task<ResultDto<long>> Add(PurchaseDetailsRequest viewModel);
        Task<ResultDto<IEnumerable<PurchaseDetailsResponse>>> GetAll();
        Task<ResultDto<PurchaseDetailsResponse>> GetById(long Id);
    }
}
