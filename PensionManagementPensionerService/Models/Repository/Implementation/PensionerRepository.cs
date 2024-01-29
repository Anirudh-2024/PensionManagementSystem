using Microsoft.EntityFrameworkCore;
using PensionManagementPensionerService.Models.Context;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using PensionManagementUserLoginService.Models;
using PensionManagementUserLoginService.Models.Context;

namespace PensionManagementPensionerService.Models.Repository.Implementation
{
    public class PensionerRepository : IPensionerRepository
    {
        private readonly AppDbContext _appDbContext;

        public PensionerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PensionerDetails> AddPensionerDetails(PensionerDetails pensionerDetails)
        {
            var result = await _appDbContext.PensionerDetails.AddAsync(pensionerDetails);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeletePensionerDetailsById(Guid pensionerId)
        {
            var result = await _appDbContext.PensionerDetails.FirstOrDefaultAsync(id => id.PensionerId == pensionerId);
            _appDbContext.PensionerDetails.Remove(result);     
        }

        public async Task<IEnumerable<PensionerDetails>> GetAllPensionerDetails()
        { 
            return await _appDbContext.PensionerDetails.ToListAsync();
        }

        public async Task<PensionerDetails> GetPensionerDetailsById(Guid pensionerId)
        {
            return await _appDbContext.PensionerDetails.FirstOrDefaultAsync(id => id.PensionerId == pensionerId);
        }

        public async Task<PensionerDetails> UpdatePensionerDetailsById(Guid pensionerId, PensionerDetails pensionerDetails)
        {
            var result = await _appDbContext.PensionerDetails.FirstOrDefaultAsync(id => id.PensionerId == pensionerId);
            if (result != null)
              {
                result.FullName = pensionerDetails.FullName;
                result.PhoneNumber = pensionerDetails.PhoneNumber;
                result.DateOfBirth = pensionerDetails.DateOfBirth;
                result.Gender = pensionerDetails.Gender;
                 result.Age = pensionerDetails.Age;
                 result.Address = pensionerDetails.Address;
                 result.AadharNumber= pensionerDetails.AadharNumber;
               await _appDbContext.SaveChangesAsync();
               return result;

           }
           return null;
        }
    }
}
