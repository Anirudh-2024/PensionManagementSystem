using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using PensionManagementPensionerService.Models;
using PensionManagementPensionerService.DTO;
using PensionManagementPensionerService.ExceptionalHandling;
using PensionManagementPensionerService.Models.Repository.Implementation;

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
                if (result.Count() == 0)
                {
                    throw new PensionerServiceException("No pensioner details found.");
                }
                return Ok(result);
            }
            catch (PensionerServiceException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet("GetPensionerDetailsById")]
        public async Task<IActionResult> GetPensionerDetailsById(Guid pensionerId)
        {
            try
            {
                var result = await _pensionerRepository.GetPensionerDetailsById(pensionerId);
                if (result == null)
                {
                    throw new PensionerServiceException("No pensioner details found for the given pensionerID.");
                }
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
            catch (PensionerServiceException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }
        [HttpGet("GetPensionerIdById")]
        public async Task<ActionResult> GetPensionerIdById(string userId)
        {
            try
            {
                var result = await _pensionerRepository.GetPensionerIdById(userId);
                if (result == null)
                {
                    throw new PensionerServiceException("No pensioner id found for the given userID.");
                }
                return Ok(result);
            }
            catch (PensionerServiceException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost("AddPensionerDetails")]
        public async Task<ActionResult<PensionerDetails>> AddPensionerDetails([FromBody] PensionerRequestDTO pensionerDetails)
        {
            try
            {
                var existingdetails = _pensionerRepository.GetPensionerIdById(pensionerDetails.Id);
                if (existingdetails != null)
                {
                    throw new PensionerServiceException("A pensioner with the same details already exists.");
                }
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
            catch (PensionerServiceException ex)
            {
                return StatusCode(409, ex.Message);
            }

        }

        [HttpPut("UpdatePensionerDetailsById")]
        public async Task<IActionResult> UpdatePensionerDetailsById(Guid pensionerId, [FromBody] PensionerRequestDTO pensionerDetails)
        {
            try
            {
                var pensioner = await _pensionerRepository.GetPensionerDetailsById(pensionerId);
                if (pensioner == null)
                {
                    throw new PensionerServiceException("No pensioner details found for the given pensionerID.");

                }
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
            catch (PensionerServiceException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpDelete("DeletePensionerById")]
        public async Task<IActionResult> DeletePensionerById(Guid pensionerId)
        {
            try
            {
                var result = await _pensionerRepository.GetPensionerDetailsById(pensionerId);
                if (result == null)
                {
                    throw new PensionerServiceException("No pensioner details found for the given pensionerID.");
                }
                _pensionerRepository.DeletePensionerDetailsById(pensionerId);
                return NoContent();
            }
            catch (PensionerServiceException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

    }
}
