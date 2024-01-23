using PensionManagementPensionerService.Models.Repository.Interfaces;

namespace PensionManagementPensionerService.Models.Repository.Implementation
{
    public class PensionPlanRepository : IPensionPlanRepository
    {
        public Task<PensionPlanDetails> AddPensionPlan(PensionPlanDetails pensionPlanDetails)
        {
            throw new NotImplementedException();
        }

        public Task<PensionPlanDetails> DeletePensionPlanById(Guid pensionPlanId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PensionPlanDetails>> GetAllPensionPlans()
        {
            throw new NotImplementedException();
        }

        public Task<PensionPlanDetails> GetPensionPlanById(int pensionPlanId)
        {
            throw new NotImplementedException();
        }

        public Task<PensionPlanDetails> UpdatePensionPlan(PensionPlanDetails pensionPlanDetails)
        {
            throw new NotImplementedException();
        }
    }
}
