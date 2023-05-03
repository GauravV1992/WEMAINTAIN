using Repositories.Common;
using System.ComponentModel.DataAnnotations;

namespace Repositories
{
   public class Vendor : BaseEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public string City { get; set; }
        public string GST { get; set; }
        public string Ext { get; set; }
        public string Pincode { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }

    }
}
