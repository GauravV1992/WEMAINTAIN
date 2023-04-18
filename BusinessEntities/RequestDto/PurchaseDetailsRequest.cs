using BusinessEntities.Common;

namespace BusinessEntities.RequestDto
{
    public class PurchaseServicesRequest
    {
        public int Id { get; set; }
        public int PurchaseDetailsId { get; set; }
        public int ServiceId { get; set; }
        public DateTime StarteDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AMCPeriod { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
    }
}
