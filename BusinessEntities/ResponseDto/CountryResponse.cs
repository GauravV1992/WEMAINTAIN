using BusinessEntities.Common;

namespace BusinessEntities.ResponseDto
{
    public class CountryResponse : BaseResponse
    {
        public int Id { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }
    }
}
