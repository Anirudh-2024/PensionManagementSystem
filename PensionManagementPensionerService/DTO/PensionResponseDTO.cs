using PensionManagementPensionerService.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PensionManagementPensionerService.DTO
{
    public class PensionResponseDTO
    {
        public Guid pensionerId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string AadharNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
        public Guid PensionPlanId { get; set; }
        [ForeignKey("PensionPlanId")]
        public virtual PensionPlanDetails PensionPlanDetails { get; set; }
    }
}
