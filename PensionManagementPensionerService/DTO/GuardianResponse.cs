namespace PensionManagementPensionerService.DTO
{
    public class GuardianResponse
    {
        public Guid GuardianId { get; set; }
        public string GuardianName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Relation { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public Guid PensionerId { get; set; }
    }
}
