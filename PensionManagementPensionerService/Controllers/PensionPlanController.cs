using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PensionManagementPensionerService.Models;
using PensionManagementPensionerService.Models.Repository.Interfaces;

namespace PensionManagementPensionerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionPlanController : ControllerBase
    {
        private readonly IPensionPlanRepository _pensionPlanRepository ;

        public PensionPlanController(IPensionPlanRepository pensionPlanRepository)
        {
            _pensionPlanRepository = pensionPlanRepository ;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PensionPlanDetails>>> GetAllPensionPlans()
        {
            try
            {
                var result = await _pensionPlanRepository.GetAllPensionPlans();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{pensionPlanId}")]
        public async Task<IActionResult> GetPensionPlanById(Guid pensionPlanId)
        {
            try
            {
                var result = await _pensionPlanRepository.GetPensionPlanById(pensionPlanId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PensionPlanDetails>> AddPensionPlan([FromBody] PensionPlanDetails pensionPlanDetails)
        {
            try
            {
                var result = await _pensionPlanRepository.AddPensionPlan(pensionPlanDetails);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePensionPlanById(Guid pensionPlanId, [FromBody] PensionPlanDetails pensionPlanDetails)
        {
            try
            {
                var result = await _pensionPlanRepository.UpdatePensionPlanById(pensionPlanId,pensionPlanDetails);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{pensionPlanId}")]
        public async Task<IActionResult> DeletePensionPlanById(Guid pensionPlanId)
        {
            try
            {
                _pensionPlanRepository.DeletePensionPlanById(pensionPlanId);
                return NoContent();
             }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
