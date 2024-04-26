using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PensionManagementPensionerService.ExceptionalHandling;
using PensionManagementPensionerService.Models;
using PensionManagementPensionerService.Models.Repository.Implementation;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using static PensionManagementPensionerService.ExceptionalHandling.PensionerServiceException;

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

        [HttpGet("GetAllPensionPlans")]
        public async Task<ActionResult<IEnumerable<PensionPlanDetails>>> GetAllPensionPlans()
        {
            try
            {
                var result = await _pensionPlanRepository.GetAllPensionPlans();
                if (result.Count() == 0)
                {
                    throw new PensionerServiceException("No pensionplan details found.");
                }
                return Ok(result);
            }
            catch (PensionerServiceException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet("GetPensionPlanById")]
        public async Task<IActionResult> GetPensionPlanById(Guid pensionPlanId)
        {
            try
            {
                var result = await _pensionPlanRepository.GetPensionPlanById(pensionPlanId);
                if (result == null)
                {
                    throw new PensionerServiceException("No pension plan details found for the given pensionPlanID.");
                }
                return Ok(result);
            }
            catch (PensionerServiceException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost("AddPensionPlan")]
        public async Task<ActionResult<PensionPlanDetails>> AddPensionPlan([FromBody] PensionPlanDetails pensionPlanDetails)
        {
            try
            {
                var existingdetails = _pensionPlanRepository.GetPensionPlanById(pensionPlanDetails.PensionPlanId);
                if (existingdetails != null)
                {
                    throw new PensionerServiceException("A pension Plan with the same details already exists.");
                }
                var result = await _pensionPlanRepository.AddPensionPlan(pensionPlanDetails);
                return Ok(result);

            }
            catch (PensionerServiceException ex)
            {
                return StatusCode(409, ex.Message);
            }

        }

        [HttpPut("UpdatePensionPlanById")]
        public async Task<IActionResult> UpdatePensionPlanById(Guid pensionPlanId, [FromBody] PensionPlanDetails pensionPlanDetails)
        {
            try
            {
                var pensionPlan = await _pensionPlanRepository.GetPensionPlanById(pensionPlanId);
                if (pensionPlan == null)
                {
                    throw new PensionerServiceException("No pensionPlan details found for the given pensionPlanID.");

                }
                var result = await _pensionPlanRepository.UpdatePensionPlanById(pensionPlanId,pensionPlanDetails);
                return Ok(result);
            }
            catch(PensionerServiceException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpDelete("DeletePensionPlanById")]
        public async Task<IActionResult> DeletePensionPlanById(Guid pensionPlanId)
        {
            try
            {
                var result = await _pensionPlanRepository.GetPensionPlanById(pensionPlanId);
                if(result == null)
                {
                    throw new PensionerServiceException("No pension plan details found for the given pensionplanID.");
                }
                _pensionPlanRepository.DeletePensionPlanById(pensionPlanId);
                return NoContent();
             }
            catch (PensionerServiceException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

    }
}
