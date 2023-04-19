using System;

namespace BusinessEntities.Common
{
   public class FilterRequest
    {
        public int PackageId { get; set; }
        public int SubPackageId { get; set; }
        public int ServiceId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}
