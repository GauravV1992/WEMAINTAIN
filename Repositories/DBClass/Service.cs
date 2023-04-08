using Repositories.Common;
using System.ComponentModel.DataAnnotations;

namespace Repositories
{
   public class Service : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
