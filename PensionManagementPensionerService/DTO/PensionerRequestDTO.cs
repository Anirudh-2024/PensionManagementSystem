namespace PensionManagementPensionerService.DTO
{
    public class PensionerRequestDTO
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string AadharNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
        public Guid PensionPlanId {  get; set; }

    }
}
