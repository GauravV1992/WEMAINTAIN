namespace Repositories.Common
{
    public class BaseEntity
    {
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int TotalRecords { get; set; }
    }
}
