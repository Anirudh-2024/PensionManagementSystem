using PensionManagementPensionerService.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PensionManagementBankingService.Models
{
    public class BankingDetails
    {
        [Key]
        public Guid BankId { get; set; }

        [Required(ErrorMessage = "Bank name is required")]
        [StringLength(100, ErrorMessage = "Bank name cannot exceed 100 characters")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Account number is required")]
        [RegularExpression(@"^\d{11}$", ErrorMessage ="Invalid account number")]
        public string AccountNumber { get; set;}

        [Required(ErrorMessage = "Ifsc code required")]
        [StringLength(11, MinimumLength = 11,ErrorMessage ="Ifsc code must be 11 characters long")]
        public string IfscCode { get; set;}


        [Required(ErrorMessage = "BranchName is required")]
        [StringLength(100, ErrorMessage = "Branch name cannot exceed 100 characters")]
        public string BranchName{ get; set;}

        [Required(ErrorMessage = "PanNumber is required")]
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]$", ErrorMessage = "Invalid PAN number")]
        public string PanNumber { get; set;}

    
        public Guid PensionerId { get; set; }

        [ForeignKey("PensionerId")]
        public virtual PensionerDetails PensionerDetails { get; set; }


    }
}
