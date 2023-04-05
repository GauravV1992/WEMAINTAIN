using BusinessEntities.Common;

namespace BusinessEntities.ResponseDto
{
    public class CategoryResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
