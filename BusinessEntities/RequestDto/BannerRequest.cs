using BusinessEntities.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.RequestDto
{
    public class BannerRequest : BaseRequest
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public string? Ext { get; set; }
    }
}
