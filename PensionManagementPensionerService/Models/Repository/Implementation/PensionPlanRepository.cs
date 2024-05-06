using Microsoft.EntityFrameworkCore;
using PensionManagementPensionerService.ExceptionalHandling;
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
            try
            {
                var existingRecord = await _appDbContext.PensionPlanDetails.FirstOrDefaultAsync( u => u.PensionPlanName == pensionPlanDetails.PensionPlanName);
                if (existingRecord != null)
                {
                    throw new DuplicateRecordException("A duplicate record already exists with the same Pension Plan Name");
                }
                PensionPlanDetails addPensionPlan = new PensionPlanDetails
                {
                    PensionPlanId = Guid.NewGuid(),
                    PensionPlanName = pensionPlanDetails.PensionPlanName,
                    Amount = pensionPlanDetails.Amount,
                    StartDate = pensionPlanDetails.StartDate,
                    EndDate = pensionPlanDetails.EndDate,
                    PensionDetails = pensionPlanDetails.PensionDetails

                };
                var result = await _appDbContext.PensionPlanDetails.AddAsync(addPensionPlan);
                await _appDbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeletePensionPlanById(Guid pensionPlanId)
        {
            try
            {
                var result = _appDbContext.PensionPlanDetails.FirstOrDefault(id => id.PensionPlanId == pensionPlanId);
                if (result == null)
                {
                    throw new NotFoundException("Pension Plan Details not found for the provided Pension Plan Id.");

                }
                _appDbContext.PensionPlanDetails.Remove(result);
                _appDbContext.SaveChanges();
            }
           
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<PensionPlanDetails>> GetAllPensionPlans()
        {
            try
            {
                var result = await _appDbContext.PensionPlanDetails.ToListAsync();
                if (result.Count == 0)
                {
                    throw new EmptyResultException("There are no pension plan details available in the database.");
                }
                return result;
            }
            
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PensionPlanDetails> GetPensionPlanById(Guid pensionPlanId)
        {
            try
            {
                var result = await _appDbContext.PensionPlanDetails.FirstOrDefaultAsync(id => id.PensionPlanId == pensionPlanId);
                if (result == null)
                {
                    throw new NotFoundException("Pension plan details not found for the provided pension plan Id.");

                }
                return result;
            }
            
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PensionPlanDetails> UpdatePensionPlanById(Guid pensionPlanId, PensionPlanDetails pensionPlanDetails)
        {
            try
            {
                var result = await _appDbContext.PensionPlanDetails.FirstOrDefaultAsync(id => id.PensionPlanId == pensionPlanId);
                if (result == null)
                {
                    throw new NotFoundException("Pension plan Details not found for the provided pension plan Id.");

                }
                result.PensionPlanName = pensionPlanDetails.PensionPlanName;
                result.Amount = pensionPlanDetails.Amount;
                result.EndDate = pensionPlanDetails.EndDate;
                result.StartDate = pensionPlanDetails.StartDate;
                result.PensionDetails = pensionPlanDetails.PensionDetails;
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
