using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using PensionManagementPensionerService.Models;
using PensionManagementPensionerService.DTO;
using PensionManagementPensionerService.Models.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using PensionManagementPensionerService.ExceptionalHandling;

namespace PensionManagementPensionerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardianController : ControllerBase
    {
        private readonly IGuardianRepository _guardianRepository;
        private readonly ILogger<GuardianController> _logger;

        public GuardianController(IGuardianRepository guardianRepository, ILogger<GuardianController> logger)
        {
            _guardianRepository = guardianRepository;
            _logger = logger;
        }

        [HttpGet("GetAllGuardianDetails")]
        public async Task<ActionResult<IEnumerable<GuardianDetails>>> GetAllGuardianDetails()
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve all guardian details.");
                var result = await _guardianRepository.GetAllGuardianDetails();
                _logger.LogInformation("Successfully retrieved all guardian details {@result}", result);
                return Ok(result);
            }
            catch (EmptyResultException ex)
            {
                _logger.LogError("Empty result returned while retrieving guardian details");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpGet("GetGuardianDetailsById")]
        public async Task<IActionResult> GetGuardianDetailsById(Guid guardianId)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve guardian details by guardian Id.");
                var result = await _guardianRepository.GetGuardianById(guardianId);
                _logger.LogInformation("Successfully retrieved guardian details by guardian Id: {@result}", result);
                var response = new GuardianResponseDTO
                {
                    GuardianId = result.GuardianId,
                    GuardianName = result.GuardianName,
                    DateOfBirth= result.DateOfBirth,
                    Relation= result.Relation,
                    Age= result.Age,
                    Gender= result.Gender,
                    PhoneNumber= result.PhoneNumber,
                    PensionerId= result.PensionerId,

                };
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError("No guardian details found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpPost("AddGuardianDetails")]
        public async Task<ActionResult<GuardianDetails>> AddGuardian([FromBody] GuardianRequestDTO guardianDetails)
        {
            try
            {
                _logger.LogInformation("Attempting to add guardian details.");
                var request = new GuardianDetails
                {
                    GuardianName = guardianDetails.GuardianName,
                    DateOfBirth = guardianDetails.DateOfBirth,
                    Relation = guardianDetails.Relation,
                    Age = guardianDetails.Age,
                    Gender = guardianDetails.Gender,
                    PhoneNumber = guardianDetails.PhoneNumber,
                    PensionerId = guardianDetails.PensionerId,
                };

                var result = await _guardianRepository.AddGuardian(request);
                _logger.LogInformation("Successfully added guardian details : {@result}", result);
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

        [HttpPut("UpdateGuardianById")]
        public async Task<IActionResult> UpdateGuardianById(Guid guardianId, [FromBody] GuardianRequestDTO guardianDetails)
        {
            try
            {
                _logger.LogInformation("Attempting to update guardian details by guardian Id.");
                var request = new GuardianDetails
                {
                    GuardianName = guardianDetails.GuardianName,
                    DateOfBirth = guardianDetails.DateOfBirth,
                    Relation = guardianDetails.Relation,
                    Age = guardianDetails.Age,
                    Gender = guardianDetails.Gender,
                    PhoneNumber = guardianDetails.PhoneNumber,
                    PensionerId = guardianDetails.PensionerId,
                };
                
                var result = await _guardianRepository.UpdateGuardianById(guardianId, request);
                _logger.LogInformation("Successfully updated guardian details by guardian Id {@result}", result);
                var response = new GuardianResponse
                {
                    GuardianId = result.GuardianId,
                    GuardianName = result.GuardianName,
                    DateOfBirth = result.DateOfBirth,
                    Relation = result.Relation,
                    Age = result.Age,
                    Gender = result.Gender,
                    PhoneNumber = result.PhoneNumber,
                    PensionerId = result.PensionerId,
                };
                
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError("No guardian details found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpDelete("DeleteGuardianById")]
        public async Task<IActionResult> DeleteGuardianById(Guid guardianId)
        {
            try
            {
                _logger.LogInformation("Attempting to delete guardian details by guardian Id.");
                _guardianRepository.DeleteGuardianById(guardianId);
                _logger.LogInformation("Successfully deleted guardian details");
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                _logger.LogError("No guardian details found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpGet ("GetGuardianIdByPensionerId")]
        public async Task<ActionResult> GetGuardianIdByPensionerId(Guid pensionerId)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve guardianId by pensioner Id.");
                var result = await _guardianRepository.GetGuadianIdByPensionerId(pensionerId);
                _logger.LogInformation("Successfully retrieved guardianId by pensioner Id: {@result}", result);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError("No guardian details found.");
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
