using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;

namespace Repositories.Interface
{
    public interface IPurchaseDetailsRepository
    {
        Task<long> Add(PurchaseDetailsRequest viewModel);
        //Task<IEnumerable<PurchaseDetails>> GetAll(PurchaseDetailsRequest request);
        Task<PurchaseDetailsWithServicesResponse> GetAll(PurchaseDetailsRequest request);
        Task<PurchaseDetails> GetById(long Id);
        Task<IEnumerable<PurchaseServices>> PurchaseServicesById(long Id);
    }
}
