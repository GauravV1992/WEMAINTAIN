using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.ResponseDto
{
    public class BillingAndCartDetailsResponse
    {
        public BillingAndCartDetailsResponse()
        {
            SubPackageServicePrices = new List<SubPackageServicePriceRequest>();
            User = new UserResponse();
        }
        public  UserResponse User { get;set;}
        public IEnumerable<SubPackageServicePriceRequest> SubPackageServicePrices { get; set; }

        public decimal TotalPackageAmount { get; set; }
        public decimal TotalDiscount { get; set; }
      
        public int SubPackageId { get; set; }
        public string AMCPeriod { get; set; }
        public string ServicesIds { get; set; }

    }
}
