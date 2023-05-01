using Repositories.Common;
using System.ComponentModel.DataAnnotations;

namespace Repositories
{
   public class Package : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ext { get; set; }
    }
}
