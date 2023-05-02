using Repositories.Common;
using System.ComponentModel.DataAnnotations;

namespace Repositories
{
   public class SubPackage : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string TermsAndCondition { get; set; }
        public string Ext { get; set; }
    }
}
