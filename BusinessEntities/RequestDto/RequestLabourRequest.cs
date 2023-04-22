using BusinessEntities.Common;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.RequestDto
{
    public class RequestLabourRequest :BaseRequest 
    {
        public int Id { get; set; }
        public int? PackageId { get; set; }
        public int? SubPackageId { get; set; }
        public int? ServiceId { get; set; }
        public int? UserId { get; set; }
        public int? LabourCount { get; set; }
        public DateOnly? AssignDate { get; set; }
        public string? Status { get; set; }
        public DateOnly? CompletedDate { get; set; }

    }
}
