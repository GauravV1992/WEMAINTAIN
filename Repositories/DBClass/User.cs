using Repositories.Common;
using System.ComponentModel.DataAnnotations;

namespace Repositories
{
   public class User : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
    }
}
