﻿using BusinessEntities.Common;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.RequestDto
{
    public class LoginRequest
    {
        public string MobileNo { get; set; }
        public string Password { get; set; }
    
    }
}