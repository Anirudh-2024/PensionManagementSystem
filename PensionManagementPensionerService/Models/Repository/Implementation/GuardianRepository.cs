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
            
            GuardianDetails addGuardian = new GuardianDetails
            {
                GuardianId = Guid.NewGuid(),
                GuardianName = guardianDetails.GuardianName,
                DateOfBirth = guardianDetails.DateOfBirth,
                Relation = guardianDetails.Relation,
                Age = guardianDetails.Age,
                Gender = guardianDetails.Gender,
                PhoneNumber = guardianDetails.PhoneNumber,
                PensionerId = guardianDetails.PensionerId,

            };
            var result = await _appDbContext.GuardianDetails.AddAsync(addGuardian);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public void DeleteGuardianById(Guid guardianId)
        {
            var result = _appDbContext.GuardianDetails.FirstOrDefault(id => id.GuardianId == guardianId);
            _appDbContext.GuardianDetails.Remove(result);
            _appDbContext.SaveChanges();
        }

        public async Task<IEnumerable<GuardianDetails>> GetAllGuardianDetails()
        {
            return await _appDbContext.GuardianDetails.Include(o => o.PensionerDetails).ThenInclude(o => o.UserDetails).Include(o => o.PensionerDetails.PensionPlanDetails).ToListAsync();
        }

        public async Task<GuardianDetails> GetGuardianById(Guid guardianId)
        {
            return await _appDbContext.GuardianDetails.Include(o => o.PensionerDetails).ThenInclude(o => o.UserDetails).Include(o => o.PensionerDetails.PensionPlanDetails).FirstOrDefaultAsync(id => id.GuardianId == guardianId);
        }

        public async Task<GuardianDetails> UpdateGuardianById(Guid guardianId, GuardianDetails guardianDetails)
        {
            var result = await _appDbContext.GuardianDetails.Include(o => o.PensionerDetails).ThenInclude(o => o.UserDetails).Include(o => o.PensionerDetails.PensionPlanDetails).FirstOrDefaultAsync(id => id.GuardianId == guardianId);
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
