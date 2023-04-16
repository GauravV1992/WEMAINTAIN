using BusinessEntities.RequestDto;

namespace Repositories.Interface
{
    public interface IPurchaseDetailsRepository
    {
        Task<long> Add(PurchaseDetailsRequest viewModel);
        Task<IEnumerable<PurchaseDetails>> GetAll(PurchaseDetailsRequest request);
        Task<PurchaseDetails> GetById(long Id);
    }
}
