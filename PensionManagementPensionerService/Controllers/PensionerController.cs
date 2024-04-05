using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using PensionManagementPensionerService.Models;
using PensionManagementPensionerService.DTO;
using PensionManagementPensionerService.ExceptionalHandling;
using Microsoft.EntityFrameworkCore;

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
            catch (EmptyResultException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpGet("GetPensionerDetailsById")]
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
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }
        [HttpGet("GetPensionerIdById")]
        public async Task<ActionResult> GetPensionerIdById(string userId)
        {
            try
            {
                var result = await _pensionerRepository.GetPensionerIdById(userId);
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

        [HttpPost("AddPensionerDetails")]
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
            catch(DuplicateRecordException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }

        }

        [HttpPut("UpdatePensionerDetailsById")]
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
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
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
            catch(NotFoundException ex)
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
