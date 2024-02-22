namespace PensionManagementUserLoginService.DTO
{
    public class LoginRespondDTO
    {
        public String Email { get; set; }

        public string Token { get; set; }

        public List<string> Roles { get; set; }
    }
}
