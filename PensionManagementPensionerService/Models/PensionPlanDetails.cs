using System.ComponentModel.DataAnnotations;

namespace PensionManagementPensionerService.Models
{
    public class PensionPlanDetails
    {
        [Key]
        public Guid PensionPlanId { get; set; }
        [Required(ErrorMessage = "PensionPlan is required")]
        
        public string PensionPlanName { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a Positive Number")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "StartDate is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "EndDate is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string PensionDetails { get; set; }
    }
}
