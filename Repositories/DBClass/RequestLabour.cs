using Repositories.Common;
using System.ComponentModel.DataAnnotations;

namespace Repositories
{
   public class RequestLabour : BaseEntity
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int SubPackageId { get; set; }
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public string PackageName { get; set; }
        public string SubPackageName { get; set; }
        public string ServiceName { get; set; }
        public string UserName { get; set; }
        public int LabourCount { get; set; }
        public string AssignDate { get; set; }
        public string Status { get; set; }
        public string CompletedDate { get; set; }
    }
}
