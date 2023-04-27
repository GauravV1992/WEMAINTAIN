using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.ResponseDto
{
    public class SubPackageServicePriceRequest
    {
        public int SubPackageId { get; set; }
        public int ServiceId { get; set; }
        public string? AMCPeriod { get; set; }
        public decimal Discount { get; set; }
        public decimal PackageAmount { get; set; }
        public string? ServiceName { get; set; }

    }
}
