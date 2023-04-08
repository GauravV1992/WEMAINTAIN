﻿using Repositories.Common;
using System.ComponentModel.DataAnnotations;

namespace Repositories
{
   public class PackageRate : BaseEntity
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int SubPackageId { get; set; }
        public int ServiceId { get; set; }
        public string PackageName { get; set; }
        public string SubPackageName { get; set; }
        public string ServiceName { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal PackageAmount { get; set; }
    }
}