using System.ComponentModel.DataAnnotations;

namespace PensionManagementPensionerService.Models
{
    public class PensionPlanDetails
    {
        [Key]
        public Guid PensionPlanId { get; set; }
        public string PensionPlanName { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PensionDetails { get; set; }
    }
}
