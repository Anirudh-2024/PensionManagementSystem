using PensionManagementUserLoginService.Models;

namespace PensionManagementPensionerService.Models.Repository.Interfaces
{
    public interface IPensionPlanRepository
    {
        Task<IEnumerable<PensionPlanDetails>> GetAllPensionPlans();

        Task<PensionPlanDetails> GetPensionPlanById(Guid pensionPlanId);

        Task<PensionPlanDetails> AddPensionPlan(PensionPlanDetails pensionPlanDetails);

        Task<PensionPlanDetails> UpdatePensionPlanById(Guid pensionPlanId, PensionPlanDetails pensionPlanDetails);

        void DeletePensionPlanById(Guid pensionPlanId);
    }
}
