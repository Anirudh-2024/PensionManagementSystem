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
            this._appDbContext = appDbContext;
        }
        public async Task<PensionPlanDetails> AddPensionPlan(PensionPlanDetails pensionPlanDetails)
        {
            var result = await _appDbContext.PensionPlanDetails.AddAsync(pensionPlanDetails);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PensionPlanDetails> DeletePensionPlanById(Guid pensionPlanId)
        {
            var result = await _appDbContext.PensionPlanDetails.FirstOrDefaultAsync(id => id.PensionPlanId == pensionPlanId);
            if (result != null)
            {
                _appDbContext.PensionPlanDetails.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<PensionPlanDetails>> GetAllPensionPlans()
        {
            return await _appDbContext.PensionPlanDetails.ToListAsync();
        }

        public async Task<PensionPlanDetails> GetPensionPlanById(Guid pensionPlanId)
        {
            return await _appDbContext.PensionPlanDetails.FirstOrDefaultAsync(id => id.PensionPlanId == pensionPlanId);
        }

        public async Task<PensionPlanDetails> UpdatePensionPlan(PensionPlanDetails pensionPlanDetails)
        {
            var result = await _appDbContext.PensionPlanDetails.FirstOrDefaultAsync(id => id.PensionPlanId == pensionPlanDetails.PensionPlanId);
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
