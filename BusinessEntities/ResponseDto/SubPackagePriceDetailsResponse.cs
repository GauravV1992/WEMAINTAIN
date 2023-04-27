using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.ResponseDto
{
    public class SubPackagePriceDetailsResponse
    {
        public SubPackagePriceDetailsResponse()
        {
            SubPackageServicePrices = new List<SubPackageServicePriceRequest>();
            SubPackages = new SubPackageResponse();
        }
        public  SubPackageResponse SubPackages { get;set;}
        public IEnumerable<SubPackageServicePriceRequest> SubPackageServicePrices { get; set; }

    }
}
