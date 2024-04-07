using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using PensionManagementPensionerService.Models;
using PensionManagementPensionerService.DTO;
using PensionManagementPensionerService.ExceptionalHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace PensionManagementPensionerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerController : ControllerBase
    {
        private readonly IPensionerRepository _pensionerRepository;
        private readonly ILogger<PensionerController> _logger;

        public PensionerController(IPensionerRepository pensionerRepository, ILogger<PensionerController> logger)
        {
            _pensionerRepository = pensionerRepository;
            _logger = logger;
        }

        [HttpGet("GetAllPensionerDetails")]
        public async Task<ActionResult<IEnumerable<PensionerDetails>>> GetAllPensionerDetails()
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve all pensioner details.");
                var result = await _pensionerRepository.GetAllPensionerDetails();
                _logger.LogInformation("Successfully retrieved all pensioner details: {@result}", result);
                return Ok(result);
                
            }
            catch (EmptyResultException ex)
            {
                _logger.LogInformation("Empty result returned while retrieving pensioner details");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpGet("GetPensionerDetailsById")]
        public async Task<IActionResult> GetPensionerDetailsById(Guid pensionerId)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve pensioner details by pensioner Id.");
                var result = await _pensionerRepository.GetPensionerDetailsById(pensionerId);
                _logger.LogInformation("Successfully retrieved pensioner details by Pensioner Id: {@result}", result);
                var response = new PensionResponseDTO
                {
                    pensionerId = result.PensionerId,
                    FullName = result.FullName,
                    DateOfBirth = result.DateOfBirth,
                    Gender = result.Gender,
                    AadharNumber = result.AadharNumber,
                    PhoneNumber = result.PhoneNumber,
                    Address = result.Address,
                    Age = result.Age,
                    Id = result.Id,
                    PensionPlanId = result.PensionPlanId,
                    PensionPlanDetails = result.PensionPlanDetails,

                };
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation("No pensioner details found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }
        [HttpGet("GetPensionerIdById")]
        public async Task<ActionResult> GetPensionerIdById(string userId)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve pensioner Id by userId.");
                var result = await _pensionerRepository.GetPensionerIdById(userId);
                _logger.LogInformation("Successfully retrieved pensioner Id by userId : {@result}", result);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation("No pensioner details found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpPost("AddPensionerDetails")]
        public async Task<ActionResult<PensionerDetails>> AddPensionerDetails([FromBody] PensionerRequestDTO pensionerDetails)
        {
            try
            {
                _logger.LogInformation("Attempting to add pensioner details.");
                var request = new PensionerDetails
                {
                    FullName = pensionerDetails.FullName,
                    DateOfBirth = pensionerDetails.DateOfBirth,
                    Gender = pensionerDetails.Gender,
                    AadharNumber=pensionerDetails.AadharNumber,
                    PhoneNumber = pensionerDetails.PhoneNumber,
                    Address = pensionerDetails.Address, 
                    Age = pensionerDetails.Age,
                    Id = pensionerDetails.Id,
                    PensionPlanId = pensionerDetails.PensionPlanId

                };

                var result = await _pensionerRepository.AddPensionerDetails(request);
                _logger.LogInformation("Successfully added pensioner details. {@result}", result);
                return Ok(result);

            }
            catch(DuplicateRecordException ex)
            {
                _logger.LogInformation("Attempted to add a duplicate record");
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }

        }

        [HttpPut("UpdatePensionerDetailsById")]
        public async Task<IActionResult> UpdatePensionerDetailsById(Guid pensionerId, [FromBody] PensionerRequestDTO pensionerDetails)
        {
            try
            {
                _logger.LogInformation("Attempting to update pensioner details by pensioner Id.");
                var request = new PensionerDetails
                {
                    FullName = pensionerDetails.FullName,
                    DateOfBirth= pensionerDetails.DateOfBirth,
                    Gender = pensionerDetails.Gender,
                    AadharNumber= pensionerDetails.AadharNumber,
                    PhoneNumber = pensionerDetails.PhoneNumber,
                    Address = pensionerDetails.Address,
                    Age= pensionerDetails.Age,
                    Id = pensionerDetails.Id,
                    PensionPlanId= pensionerDetails.PensionPlanId

                };
                var result = await _pensionerRepository.UpdatePensionerDetailsById(pensionerId, request);
                _logger.LogInformation("Successfully updated pensioner details by Pensioner Id {@result}", result);
                var response = new PensionResponseDTO
                {
                    pensionerId=result.PensionerId,
                    FullName = result.FullName,
                    DateOfBirth = result.DateOfBirth,
                    Gender = result.Gender,
                    AadharNumber=result.AadharNumber,
                    PhoneNumber = result.PhoneNumber,
                    Address = result.Address,
                    Age= result.Age,
                    Id = result.Id,
                    PensionPlanId=result.PensionPlanId

                };
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation("No pensioner details found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpDelete("DeletePensionerById")]
        public async Task<IActionResult> DeletePensionerById(Guid pensionerId)
        {
            try
            {
                _logger.LogInformation("Attempting to delete pensioner details by pensioner Id.");
                _pensionerRepository.DeletePensionerDetailsById(pensionerId);
                _logger.LogInformation("Successfully deleted pensioner details by Pensioner Id.");
                return NoContent();
            }
            catch(NotFoundException ex)
            {
                _logger.LogInformation("No pensioner details found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An unexpected error occurred while processing the request: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

    }
}
