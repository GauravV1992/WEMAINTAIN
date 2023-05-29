using BusinessEntities.Common;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.RequestDto
{
    public class LoginRequest
    {
        
            public string? PhoneNumber { get; set; }

        public string? Credential { get; set; }
        public string? MobileNo { get; set; }
        public string? Password { get; set; }
        public bool? IsAdmin  { get; set; }
        public string? Username { get; set; }

    }
}
