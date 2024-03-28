using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PensionManagementPensionerService.Models
{
    public class GuardianDetails
    {
        [Key]
        public Guid GuardianId { get; set; }
        [Required(ErrorMessage = "GuardianName is required")]
        public string GuardianName { get; set; }
        [Required(ErrorMessage = "DateOfBirth is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Relation is required")]
        public string Relation { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required")]
        [StringLength(10, MinimumLength = 10)]
        [Phone(ErrorMessage = "Invalid phone Number")]
        public string PhoneNumber { get; set; }
        public Guid PensionerId { get; set; }

        [ForeignKey("PensionerId")]
        public virtual PensionerDetails PensionerDetails { get; set; }
    }
}
