using Repositories.Common;
using System.ComponentModel.DataAnnotations;

namespace Repositories
{
   public class State : BaseEntity
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }

    }
}
