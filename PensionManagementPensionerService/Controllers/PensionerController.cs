using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using PensionManagementPensionerService.Models;
using PensionManagementPensionerService.DTO;

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

        [HttpGet]
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

        [HttpGet("{pensionerId}")]
        public async Task<IActionResult> GetPensionerDetailsById(Guid pensionerId)
        {
            try
            {
                var result = await _pensionerRepository.GetPensionerDetailsById(pensionerId);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("UserId/{userId}")]
        public async Task<ActionResult> GetPensionerIdById(string userId)
        {
            try
            {
                var result = await _pensionerRepository.GetPensionerIdById(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PensionerDetails>> AddPensionerDetails([FromBody] PensionerRequestDTO pensionerDetails)
        {
            try
            {
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
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePensionerDetailsById(Guid pensionerId, [FromBody] PensionerRequestDTO pensionerDetails)
        {
            try
            {
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{pensionerId}")]
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
