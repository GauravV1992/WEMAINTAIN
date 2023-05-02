using BusinessEntities.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.RequestDto
{
    public class CategoryRequest : BaseRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Ext { get; set; }

    }
}
