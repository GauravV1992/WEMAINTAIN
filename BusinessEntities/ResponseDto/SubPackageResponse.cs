using BusinessEntities.Common;

namespace BusinessEntities.ResponseDto
{
    public class SubPackageResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string TermsAndCondition { get; set; }
    }
}
