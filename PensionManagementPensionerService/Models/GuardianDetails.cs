using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PensionManagementPensionerService.Models
{
    public class GuardianDetails
    {
        [Key]
        public Guid GuardianId { get; set; }
        public string GuardianName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Relation { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string PensionerId { get; set; }

        [ForeignKey("PensionerId")]
        public virtual PensionerDetails PensionerDetails { get; set; }
    }
}
