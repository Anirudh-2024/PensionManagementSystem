using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using PensionManagementPensionerService.Models;

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetGuardianDetailsById")]
        public async Task<IActionResult> GetGuardianDetailsById(Guid guardianId)
        {
            try
            {
                var result = await _guardianRepository.GetGuardianById(guardianId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddGuardianDetails")]
        public async Task<ActionResult<GuardianDetails>> AddGuardian([FromBody] GuardianDetails guardianDetails)
        {
            try
            {
                var result = await _guardianRepository.AddGuardian(guardianDetails);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("UpdateGuardianById")]
        public async Task<IActionResult> UpdateGuardianById(Guid guardianId, [FromBody] GuardianDetails guardianDetails)
        {
            try
            {
                var result = await _guardianRepository.UpdateGuardianById(guardianId, guardianDetails);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
