using BusinessEntities.Common;

namespace BusinessEntities.RequestDto
{
    public class CategoryRequest : BaseRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
