using BusinessEntities.Common;

namespace BusinessEntities.RequestDto
{
    public class PackageRateRequest : BaseRequest
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int SubPackageId { get; set; }
        public int ServiceId { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal PackageAmount { get; set; }
    }
}
