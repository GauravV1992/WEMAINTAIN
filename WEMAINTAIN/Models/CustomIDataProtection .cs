using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.DataProtection;

namespace WEMAINTAIN.Models
{
    public class UniqueCode
    {
        public readonly string Id = "FMSSC";
    }
    public class CustomIDataProtection
    {
        private readonly IDataProtector protector;
        public CustomIDataProtection(IDataProtectionProvider dataProtectionProvider, UniqueCode uniqueCode)
        {
            protector = dataProtectionProvider.CreateProtector(uniqueCode.Id);
        }
        public string Decode(string data)
        {
            return protector.Unprotect(data);
        }
        public string Encode(string data)
        {
            return protector.Protect(data);
           
        }
    }
}
