using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using PensionManagementPensionerService.Models;
using PensionManagementPensionerService.DTO;
using PensionManagementPensionerService.Models.Context;
using Microsoft.AspNetCore.Http.HttpResults;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuardianDetails>>> GetAllGuardianDetails()
        {
            try
            {
                var result = await _guardianRepository.GetAllGuardianDetails();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{guardianId}")]
        public async Task<IActionResult> GetGuardianDetailsById(Guid guardianId)
        {
            try
            {
                var result = await _guardianRepository.GetGuardianById(guardianId);
                var response = new GuardianResponseDTO
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{guardianId}")] 
        public async Task<IActionResult> DeleteGuardianById(Guid guardianId)
        {
            try
            {
                _guardianRepository.DeleteGuardianById(guardianId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet ("PensionerId/{pensionerId}")]
        public async Task<ActionResult> GetGuardianIdByPensionerId(Guid pensionerId)
        {
            try
            {
                var result = await _guardianRepository.GetGuadianIdByPensionerId(pensionerId);
                return Ok(result);
            }

            catch (Exception ex)
            {

                throw new Exception("Error",ex);
            }
        }
    }
}
