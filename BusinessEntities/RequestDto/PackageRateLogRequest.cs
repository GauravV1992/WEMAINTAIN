﻿using BusinessEntities.Common;

namespace BusinessEntities.RequestDto
{
    public class PackageRateLogRequest : BaseRequest
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public int SubPackageId { get; set; }
        public string SubPackageName { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal PackageAmount { get; set; }

        public string AMCPeriod { get; set; }
        // public DateTime ModifiedOn { get; set; }

    }
}