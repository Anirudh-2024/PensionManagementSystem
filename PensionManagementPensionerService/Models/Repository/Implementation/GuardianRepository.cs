using Microsoft.EntityFrameworkCore;
using PensionManagementPensionerService.Models.Context;
using PensionManagementPensionerService.Models.Repository.Interfaces;

namespace PensionManagementPensionerService.Models.Repository.Implementation
{
    public class GuardianRepository : IGuardianRepository
    {
        private readonly AppDbContext _appDbContext;

        public GuardianRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<GuardianDetails> AddGuardian(GuardianDetails guardianDetails)
        {   
            var result = await _appDbContext.GuardianDetails.AddAsync(guardianDetails);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeleteGuardianById(Guid guardianId)
        {
            var result = await _appDbContext.GuardianDetails.FirstOrDefaultAsync(id => id.GuardianId == guardianId);
            _appDbContext.GuardianDetails.Remove(result);         
        }

        public async Task<IEnumerable<GuardianDetails>> GetAllGuardianDetails()
        {
            return await _appDbContext.GuardianDetails.ToListAsync();
        }

        public async Task<GuardianDetails> GetGuardianById(Guid guardianId)
        {
            return await _appDbContext.GuardianDetails.FirstOrDefaultAsync(id => id.GuardianId == guardianId);
        }

        public async Task<GuardianDetails> UpdateGuardianById(Guid guardianId, GuardianDetails guardianDetails)
        {
            var result = await _appDbContext.GuardianDetails.FirstOrDefaultAsync(id => id.GuardianId == guardianId);
            if (result != null)
            {
               result.GuardianName= guardianDetails.GuardianName;
               result.Relation = guardianDetails.Relation;
               result.PhoneNumber = guardianDetails.PhoneNumber;
               result.Age = guardianDetails.Age;
               result.Gender = guardianDetails.Gender;
               result.DateOfBirth = guardianDetails.DateOfBirth;
               await _appDbContext.SaveChangesAsync();
               return result;
           }
            return null;
        }
    }
}
