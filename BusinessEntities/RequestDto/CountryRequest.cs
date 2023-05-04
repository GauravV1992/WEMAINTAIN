using BusinessEntities.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.RequestDto
{
    public class CountryRequest : BaseRequest
    {
        public int Id { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }

    }
}
