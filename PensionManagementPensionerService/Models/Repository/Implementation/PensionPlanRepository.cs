using Microsoft.EntityFrameworkCore;
using PensionManagementPensionerService.Models.Context;
using PensionManagementPensionerService.Models.Repository.Interfaces;

namespace PensionManagementPensionerService.Models.Repository.Implementation
{
    public class PensionPlanRepository : IPensionPlanRepository
    {
        private readonly AppDbContext _appDbContext;

        public PensionPlanRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<PensionPlanDetails> AddPensionPlan(PensionPlanDetails pensionPlanDetails)
        {
            var result = await _appDbContext.PensionPlanDetails.AddAsync(pensionPlanDetails);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeletePensionPlanById(Guid pensionPlanId)
        {
            var result = await _appDbContext.PensionPlanDetails.FirstOrDefaultAsync(id => id.PensionPlanId == pensionPlanId);
            _appDbContext.PensionPlanDetails.Remove(result); 
        }

        public async Task<IEnumerable<PensionPlanDetails>> GetAllPensionPlans()
        {
            return await _appDbContext.PensionPlanDetails.ToListAsync();
        }

        public async Task<PensionPlanDetails> GetPensionPlanById(Guid pensionPlanId)
        {
            return await _appDbContext.PensionPlanDetails.FirstOrDefaultAsync(id => id.PensionPlanId == pensionPlanId);
        }

        public async Task<PensionPlanDetails> UpdatePensionPlanById(Guid pensionPlanId, PensionPlanDetails pensionPlanDetails)
        {
            var result = await _appDbContext.PensionPlanDetails.FirstOrDefaultAsync(id => id.PensionPlanId == pensionPlanId);
            if (result != null)
            {
               result.PensionPlanName = pensionPlanDetails.PensionPlanName;
               result.Amount = pensionPlanDetails.Amount;  
               result.EndDate = pensionPlanDetails.EndDate;
               result.StartDate = pensionPlanDetails.StartDate;
               result.PensionDetails = pensionPlanDetails.PensionDetails;
               await _appDbContext.SaveChangesAsync();
               return result;

            }
            return null;
        }
    }
}
