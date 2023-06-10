﻿using BusinessEntities.Common;

namespace BusinessEntities.ResponseDto
{
    public class ProfileResponse : BaseResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }

        public string Pass { get; set; }

    }
}