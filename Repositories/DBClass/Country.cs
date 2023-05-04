using Repositories.Common;
using System.ComponentModel.DataAnnotations;

namespace Repositories
{
   public class Country : BaseEntity
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }
}
