﻿namespace PensionManagementPensionerService.Models.Repository.Interfaces
{
    public interface IPensionerRepository
    {
        Task<IEnumerable<PensionerDetails>> GetAllPensionerDetails();
        Task<PensionerDetails> GetPensionerDetailsById(Guid pensionerId);

        Task<PensionerDetails> UpdatePensionerDetails(PensionerDetails pensionerDetails);

        Task<PensionerDetails> AddPensionerDetails(PensionerDetails pensionerDetails);  

        Task<PensionerDetails> DeletePensionerDetailsById(Guid pensionerId);
            
    }
}