﻿using BusinessEntities.Common;

namespace BusinessEntities.ResponseDto
{
    public class UserResponse : BaseResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
    }
}
