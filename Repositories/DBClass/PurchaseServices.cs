
using Repositories.Common;
using System;

namespace Repositories
{
    public class PurchaseServices : BaseEntity
    {
        public int Id { get; set; }
        public int PurchaseDetailsId { get; set; }
        public int ServiceId { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string AMCPeriod { get; set; }

        public string ServiceName { get; set; }
    }
}
