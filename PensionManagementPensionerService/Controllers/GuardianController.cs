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

        public GuardianController(IGuardianRepository guardianRepository)
        {
            _guardianRepository = guardianRepository;
        }

        [HttpGet("GetAllGuardianDetails")]
        public async Task<ActionResult<IEnumerable<GuardianDetails>>> GetAllGuardianDetails()
        {
            try
            {
                var result = await _guardianRepository.GetAllGuardianDetails();
                return Ok(result);
            }
            catch (EmptyResultException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpGet("GetGuardianDetailsById")]
        public async Task<IActionResult> GetGuardianDetailsById(Guid guardianId)
        {
            try
            {
                var result = await _guardianRepository.GetGuardianById(guardianId);
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpPost("AddGuardianDetails")]
        public async Task<ActionResult<GuardianDetails>> AddGuardian([FromBody] GuardianRequestDTO guardianDetails)
        {
            try
            {
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
                return Ok(result);
            }
            catch (DuplicateRecordException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }

        }

        [HttpPut("UpdateGuardianById")]
        public async Task<IActionResult> UpdateGuardianById(Guid guardianId, [FromBody] GuardianRequestDTO guardianDetails)
        {
            try
            {
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpDelete("DeleteGuardianById")]
        public async Task<IActionResult> DeleteGuardianById(Guid guardianId)
        {
            try
            {
                _guardianRepository.DeleteGuardianById(guardianId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpGet ("GetGuardianIdByPensionerId")]
        public async Task<ActionResult> GetGuardianIdByPensionerId(Guid pensionerId)
        {
            try
            {
                var result = await _guardianRepository.GetGuadianIdByPensionerId(pensionerId);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }


        }
    }
}
