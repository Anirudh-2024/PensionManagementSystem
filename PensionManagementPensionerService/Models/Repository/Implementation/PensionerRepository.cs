using Microsoft.EntityFrameworkCore;
using PensionManagementPensionerService.Models.Context;
using PensionManagementPensionerService.Models.Repository.Interfaces;

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
            PensionerDetails addPensionerDetails = new PensionerDetails
            {
                PensionerId = Guid.NewGuid(),
                FullName = pensionerDetails.FullName,
                DateOfBirth = pensionerDetails.DateOfBirth,
                Age = pensionerDetails.Age,
                Gender = pensionerDetails.Gender,
                Address = pensionerDetails.Address,
                PensionPlanId = pensionerDetails.PensionPlanId,
                Id = pensionerDetails.Id,
                AadharNumber = pensionerDetails.AadharNumber,
                PhoneNumber = pensionerDetails.PhoneNumber,

            };
            var result = await _appDbContext.PensionerDetails.AddAsync(addPensionerDetails);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public void DeletePensionerDetailsById(Guid pensionerId)
        {
            var result = _appDbContext.PensionerDetails.FirstOrDefault(id => id.PensionerId == pensionerId);
            _appDbContext.PensionerDetails.Remove(result);
            _appDbContext.SaveChanges();
        }

        public async Task<IEnumerable<PensionerDetails>> GetAllPensionerDetails()
        { 
            return await _appDbContext.PensionerDetails.Include(o => o.PensionPlanDetails).ToListAsync();
        }

        public async Task<PensionerDetails> GetPensionerDetailsById(Guid pensionerId)
        {
            return await _appDbContext.PensionerDetails.Include(o => o.PensionPlanDetails).FirstOrDefaultAsync(id => id.PensionerId == pensionerId);
        }

        public async Task<PensionerDetails> UpdatePensionerDetailsById(Guid pensionerId, PensionerDetails pensionerDetails)
        {
            var result = await _appDbContext.PensionerDetails.Include(o => o.PensionPlanDetails).FirstOrDefaultAsync(id => id.PensionerId == pensionerId);
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
        public async Task<Guid?> GetPensionerIdById(string userId)
        {
                var pensioner = await _appDbContext.PensionerDetails
                                      .FirstOrDefaultAsync(p => p.Id == userId);
                if (pensioner != null)
                {
                    return pensioner.PensionerId;
                }

                return null;
        }
    }
}
