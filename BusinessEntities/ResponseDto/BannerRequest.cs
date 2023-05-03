using BusinessEntities.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.ResponseDto
{
    public class BannerResponse : BaseResponse
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public string Ext  { get; set; }
    }
}
