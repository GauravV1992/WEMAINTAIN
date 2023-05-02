using BusinessEntities.Common;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.ResponseDto
{
    public class LoginResponse 
    {
        public string MobileNo { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string Address { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string accessToken { get; set; }
        public string Username { get; set; }
    }
}
