using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using PensionManagementPensionerService.Models;

namespace PensionManagementPensionerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerController : ControllerBase
    {
        private readonly IPensionerRepository _pensionerRepository;

        public PensionerController(IPensionerRepository pensionerRepository)
        {
            _pensionerRepository = pensionerRepository;
        }

        [HttpGet("GetAllPensionerDetails")]
        public async Task<ActionResult<IEnumerable<PensionerDetails>>> GetAllPensionerDetails()
        {
            try
            {
                var result = await _pensionerRepository.GetAllPensionerDetails();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPensionerDetailsById")]
        public async Task<IActionResult> GetPensionerDetailsById(Guid pensionerId)
        {
            try
            {
                var result = await _pensionerRepository.GetPensionerDetailsById(pensionerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddPensionerDetails")]
        public async Task<ActionResult<PensionerDetails>> AddPensionerDetails([FromBody] PensionerDetails pensionerDetails)
        {
            try
            {
                await _pensionerRepository.AddPensionerDetails(pensionerDetails);
                return Ok(pensionerDetails);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("UpdatePensionerDetailsById")]
        public async Task<IActionResult> UpdatePensionerDetailsById(Guid pensionerId, [FromBody] PensionerDetails pensionerDetails)
        {
            try
            {
                var result = await _pensionerRepository.GetPensionerDetailsById(pensionerId);
                await _pensionerRepository.UpdatePensionerDetailsById(pensionerId, pensionerDetails);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletePensionerById")]
        public async Task<IActionResult> DeletePensionerById(Guid pensionerId)
        {
            try
            {
                _pensionerRepository.DeletePensionerDetailsById(pensionerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
