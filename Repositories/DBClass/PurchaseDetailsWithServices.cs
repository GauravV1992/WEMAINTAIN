using Repositories.Common;

namespace Repositories
{
    public class PurchaseDetailsWithServices : BaseEntity
    {
        public PurchaseDetailsWithServices()
        {
            PurchaseServices = new List<PurchaseServices>();
            PurchaseDetails = new List<PurchaseDetails>();
        }
        public IEnumerable<PurchaseServices> PurchaseServices { get; set; }
        public IEnumerable<PurchaseDetails> PurchaseDetails { get; set; }

    }
}
