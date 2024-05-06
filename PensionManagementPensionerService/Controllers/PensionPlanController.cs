using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PensionManagementPensionerService.ExceptionalHandling;
using PensionManagementPensionerService.Models;
using PensionManagementPensionerService.Models.Repository.Interfaces;

namespace PensionManagementPensionerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionPlanController : ControllerBase
    {
        private readonly IPensionPlanRepository _pensionPlanRepository ;
        private readonly ILogger<PensionPlanController> _logger;

        public PensionPlanController(IPensionPlanRepository pensionPlanRepository, ILogger<PensionPlanController> logger)
        {
            _pensionPlanRepository = pensionPlanRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PensionPlanDetails>>> GetAllPensionPlans()
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve all pension plan details.");
                var result = await _pensionPlanRepository.GetAllPensionPlans();
                _logger.LogInformation("Successfully retrieved all pensionplan details");
                return Ok(result);
            }
            catch (EmptyResultException ex)
            {
                _logger.LogError("Empty result returned while retrieving pensionplan details");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpGet("{pensionPlanId}")]
        public async Task<IActionResult> GetPensionPlanById(Guid pensionPlanId)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve pension plan details by pension plan Id.");
                var result = await _pensionPlanRepository.GetPensionPlanById(pensionPlanId);
                _logger.LogInformation("Successfully retrieved pension plan details by Pensionplan Id : {@result}", result.PensionPlanId);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError("No pension plan details found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PensionPlanDetails>> AddPensionPlan([FromBody] PensionPlanDetails pensionPlanDetails)
        {
            try
            {
                _logger.LogInformation("Attempting to add all pension plan details.");
                var result = await _pensionPlanRepository.AddPensionPlan(pensionPlanDetails);
                _logger.LogInformation("Successfully added pension plan details {@result}", result.PensionPlanId);
                return Ok(result);

            }
            catch (DuplicateRecordException ex)
            {
                _logger.LogError("Attempted to add a duplicate record");
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePensionPlanById(Guid pensionPlanId, [FromBody] PensionPlanDetails pensionPlanDetails)
        {
            try
            {
                _logger.LogInformation("Attempting to update pension plan details by pensionplan id.");
                var result = await _pensionPlanRepository.UpdatePensionPlanById(pensionPlanId,pensionPlanDetails);
                _logger.LogInformation("Successfully updated pensionplan details by Pensionplan Id: {@result}", result.PensionPlanId);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError("No pension plan details found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpDelete("{pensionPlanId}")]
        public async Task<IActionResult> DeletePensionPlanById(Guid pensionPlanId)
        {
            try
            {
                _logger.LogInformation("Attempting to delete pensionplan details by pensionplan id.");
                _pensionPlanRepository.DeletePensionPlanById(pensionPlanId);
                _logger.LogInformation("Successfully deleted pensioneplan details by Pensionplan Id {pensionPlanId}.", pensionPlanId );
                return NoContent();
             }
            catch (NotFoundException ex)
            {
                _logger.LogError("No pension plan details found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

    }
}
