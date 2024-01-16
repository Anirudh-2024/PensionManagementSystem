using PensionManagementUserLoginService.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PensionManagementPensionerService.Models
{
    public class PensionerDetails
    {
        [Key]
        public Guid PensionerId { get; set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string AadharNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserDetails UserDetails { get; set; }
        public string Address { get; set; }
        public string PensionPlanId { get; set; }

        [ForeignKey("PensionPlanId")]
        public virtual PensionPlanDetails PensionPlanDetails { get; set; }
        public int Age { get; set; }
    }
}
