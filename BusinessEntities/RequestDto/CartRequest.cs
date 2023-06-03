using BusinessEntities.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.RequestDto
{
    public class CartRequest : BaseRequest
    {
        public int SubPackageId { get; set; }
        public string AMCPeriod { get; set; }
        public string ServicesIds { get; set; }
    }
}
