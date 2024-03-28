using PensionManagementPensionerService.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PensionManagementPensionerService.DTO
{
    public class PensionResponseDTO
    {
        public Guid PensionerId { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Date Of Birth is required")]

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [StringLength(12, MinimumLength = 12)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Aadhar number must contain only digits.")]
        [Required(ErrorMessage = "AadharNumber is required")]
        public string AadharNumber { get; set; }
        [StringLength(10, MinimumLength = 10)]
        [Phone(ErrorMessage = "Invalid phone Number")]
        [Required(ErrorMessage = "PhoneNumber is required")]

        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "userId is required")]
        public string Id { get; set; }
        [Required(ErrorMessage = "PensionPlan is required")]
        public Guid PensionPlanId { get; set; }
        [ForeignKey("PensionPlanId")]
        public virtual PensionPlanDetails PensionPlanDetails { get; set; }
    }
}
