using PensionManagementUserLoginService.Models;

namespace PensionManagementPensionerService.Models.Repository.Interfaces
{
    public interface IPensionPlanRepository
    {
        Task<IEnumerable<PensionPlanDetails>> GetAllPensionPlans();

        Task<PensionPlanDetails> GetPensionPlanById(int pensionPlanId);

        Task<PensionPlanDetails> AddPensionPlan(PensionPlanDetails pensionPlanDetails);

        Task<PensionPlanDetails> UpdatePensionPlan(PensionPlanDetails pensionPlanDetails);

        Task<PensionPlanDetails> DeletePensionPlan(Guid pensionPlanId);
    }
}
