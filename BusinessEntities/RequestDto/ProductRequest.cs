using BusinessEntities.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.RequestDto
{
    public class ProductRequest : BaseRequest
    {
        public int Id { get; set; }
        public int? VendorId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public int? PackageId { get; set; }
        public int? SubPackageId { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? Price { get; set; }
        public string? GST { get; set; }
        public string? Ext { get; set; }
        public bool? ShowOnHomePage { get; set; }
        public decimal? Discount { get; set; }
        public string? GoldColor { get; set; }
        public string? GoldKT { get; set; }
        public string? GoldWeight { get; set; }
        public string? DiamondClarity { get; set; }
        public string? DiamondColor { get; set; }
        public string? DiamondWeight { get; set; }
        public string? DiamondShape { get; set; }
        public string? DiamondSize { get; set; }
        public string? MakeCountry { get; set; }
        public string? MaleFemale { get; set; }
        public bool? IsActive { get; set; }
        public string? Vendor { get; set; }
        public string? Package { get; set; }
        public string? SubPackage { get; set; }

    }
}
