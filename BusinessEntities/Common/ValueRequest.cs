
namespace BusinessEntities.Common
{
   public class ValueRequest
    {
        public long Id { get; set; }

    }

    public class OrderRequest
    {
        public string id { get; set; }

    }

    public class PaymentSuccessRequest
    {
        public string razorpay_payment_id { get; set; }

        public string razorpay_order_id { get; set; }

        public string razorpay_signature { get; set; }
        public string sOrderId { get; set; }
        public int subPackageId { get; set; }
        public string AMCPeriod { get; set; }
        public string ServicesIds { get; set; }

           public decimal PackageAmount { get; set; }
    }
        


   
}
