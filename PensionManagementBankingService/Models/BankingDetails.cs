using PensionManagementPensionerService.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PensionManagementBankingService.Models
{
    public class BankingDetails
    {
        [Key]
        public Guid BankId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set;}
        public string IfscCode { get; set;}
        public string BranchName{ get; set;}
        public string PanNumber { get; set;}

    
        public Guid PensionerId { get; set; }

        [ForeignKey("PensionerId")]
        public virtual PensionerDetails PensionerDetails { get; set; }


    }
}
