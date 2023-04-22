using BusinessEntities.Common;

namespace BusinessEntities.RequestDto
{
    public class CouponRequest : BaseRequest
    {
        public int Id { get; set; }
        public string? CouponCode { get; set; }
        public int? UserId { get; set; }
        public DateTime? CouponStartDate { get; set; }
        public DateTime? CouponEndDate { get; set; }
        public bool? IsRedeem { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public string? UserName { get; set; }

    }
}
