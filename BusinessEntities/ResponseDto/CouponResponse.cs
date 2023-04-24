using BusinessEntities.Common;

namespace BusinessEntities.ResponseDto
{
    public class CouponResponse : BaseResponse
    {
       
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public int UserId { get; set; }
      
        public string IsRedeem { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string UserName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

    }
}
