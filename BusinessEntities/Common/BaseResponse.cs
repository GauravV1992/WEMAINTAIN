namespace BusinessEntities.Common
{
   public class BaseResponse
    {
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int TotalRecords { get; set; }
    }
}
