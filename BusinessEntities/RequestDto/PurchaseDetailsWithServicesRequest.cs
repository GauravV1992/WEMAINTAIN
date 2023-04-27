using BusinessEntities.Common;
using BusinessEntities.RequestDto;

namespace BusinessEntities.RequestDto
{
    public class PurchaseDetailsWithServicesRequest
    {
        public PurchaseDetailsWithServicesRequest()
        {
            PurchaseServices = new List<PurchaseServicesRequest>();
            PurchaseDetails = new List<PurchaseDetailsRequest>();
        }
       
        public IEnumerable<PurchaseServicesRequest> PurchaseServices { get; set; }
        public IEnumerable<PurchaseDetailsRequest> PurchaseDetails { get; set; }

    }
}
