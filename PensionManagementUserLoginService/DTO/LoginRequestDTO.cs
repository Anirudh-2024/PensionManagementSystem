using System.ComponentModel.DataAnnotations;

namespace PensionManagementUserLoginService.DTO
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Email address is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage ="Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [StringLength(8, MinimumLength = 8, ErrorMessage ="Password must be 8 characters long")]
        public string Password { get; set; }
    }
}
