using BusinessEntities.Common;

namespace BusinessEntities.ResponseDto
{
    public class PurchaseDetailsWithServicesResponse
    {
        public PurchaseDetailsWithServicesResponse()
        {
            PurchaseServices = new List<PurchaseServicesResponse>();
            PurchaseDetails = new List<PurchaseDetailsResponse>();
        }
        public IEnumerable<PurchaseServicesResponse> PurchaseServices { get; set; }
        public IEnumerable<PurchaseDetailsResponse> PurchaseDetails { get; set; }

    }
}
