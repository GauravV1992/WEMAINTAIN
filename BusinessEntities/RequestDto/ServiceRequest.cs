using BusinessEntities.Common;

namespace BusinessEntities.RequestDto
{
    public class ServiceRequest : BaseRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
