using BusinessEntities.Common;

namespace BusinessEntities.ResponseDto
{
    public class PurchaseDetailsResponse : BaseResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PackageId { get; set; }
        public int SubPackageId { get; set; }
        //public int ServiceId { get; set; }
        public string PackageName { get; set; }
        public string SubPackageName { get; set; }
        //public string ServiceName { get; set; }
        //public decimal Rate { get; set; }
        //public decimal Discount { get; set; }
        public decimal PackageAmount { get; set; }
        //public string AMCPeriod { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }

    }
}
