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
                if (result.Count() == 0)
                {
                    throw new PensionerServiceException("No guardian details found.");
                }
                return Ok(result);
            }
            catch (PensionerServiceException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet("GetGuardianDetailsById")]
        public async Task<IActionResult> GetGuardianDetailsById(Guid guardianId)
        {
            try
            {
                var result = await _guardianRepository.GetGuardianById(guardianId);
                if(result == null)
                {
                    throw new PensionerServiceException("No guardian details found for the given guardianID.");
                }
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
            catch (PensionerServiceException ex)
            {
                return StatusCode(404,ex.Message);
            }
        }

        [HttpPost("AddGuardianDetails")]
        public async Task<ActionResult<GuardianDetails>> AddGuardian([FromBody] GuardianRequestDTO guardianDetails)
        {
            try
            {
                var existingdetails = _guardianRepository.GetGuadianIdByPensionerId(guardianDetails.PensionerId);
                if (existingdetails != null)
                {
                    throw new PensionerServiceException("A guardian with the same details already exists.");
                }
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
            catch (PensionerServiceException ex)
            {
                return StatusCode(409, ex.Message);
            }

        }

        [HttpPut("UpdateGuardianById")]
        public async Task<IActionResult> UpdateGuardianById(Guid guardianId, [FromBody] GuardianRequestDTO guardianDetails)
        {
            try
            {
                var guardian = await _guardianRepository.GetGuardianById(guardianId);
                if(guardian == null)
                {
                    throw new PensionerServiceException("No guardian details found for the given guardianID.");

                }
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
            catch (PensionerServiceException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpDelete("DeleteGuardianById")]
        public async Task<IActionResult> DeleteGuardianById(Guid guardianId)
        {
            try
            {
                var result = await _guardianRepository.GetGuardianById(guardianId);
                if (result == null)
                {
                    throw new PensionerServiceException("No guardian details found for the given guardianID.");
                }
                _guardianRepository.DeleteGuardianById(guardianId);
                return NoContent();
            }
            catch (PensionerServiceException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet ("GetGuardianIdByPensionerId")]
        public async Task<ActionResult> GetGuardianIdByPensionerId(Guid pensionerId)
        {
            try
            {
                var result = await _guardianRepository.GetGuadianIdByPensionerId(pensionerId);
                if (result == null)
                {
                    throw new PensionerServiceException("No guardianId found for the given pensionerID.");
                }
                return Ok(result);
            }

            catch (PensionerServiceException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }
    }
}
