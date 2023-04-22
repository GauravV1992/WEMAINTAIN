using System;

namespace BusinessEntities.Common
{
   public class BaseRequest
    {
        public BaseRequest()
        {
            CreatedBy = 101;
            StartDate = string.Empty;
            EndDate = string.Empty;
        }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public int PageIndex { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}
