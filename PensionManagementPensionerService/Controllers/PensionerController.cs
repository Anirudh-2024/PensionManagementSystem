using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using PensionManagementPensionerService.Models;
using PensionManagementPensionerService.DTO;
using PensionManagementPensionerService.ExceptionalHandling;
using PensionManagementPensionerService.Models.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;

namespace PensionManagementPensionerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerController : ControllerBase
    {
        private readonly IPensionerRepository _pensionerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PensionerController> _logger;

        public PensionerController(IPensionerRepository pensionerRepository, ILogger<PensionerController> logger, IMapper mapper)
        {
            _pensionerRepository = pensionerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PensionerDetails>>> GetAllPensionerDetails()
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve all pensioner details.");
                var result = await _pensionerRepository.GetAllPensionerDetails();
                if (result.Count() == 0)
                {
                    throw new PensionerServiceException("No pensioner details found.");
                }
                _logger.LogInformation("Successfully retrieved all pensioner details");             
                return Ok(_mapper.Map<List<PensionResponseDTO>>(result));
            }
            catch (PensionerServiceException ex)
            {
                _logger.LogError("Empty result returned while retrieving pensioner details");
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpGet("{pensionerId}")]
        public async Task<IActionResult> GetPensionerDetailsById(Guid pensionerId)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve pensioner details by pensioner Id.");
                var result = await _pensionerRepository.GetPensionerDetailsById(pensionerId);
                if (result == null)
                {
                    throw new PensionerServiceException("No pensioner details found for the given pensionerID.");
                }
                _logger.LogInformation("Successfully retrieved pensioner details by Pensioner Id: {@result}", result.PensionerId);
                return Ok(_mapper.Map<PensionResponseDTO>(result));
            }
            catch (PensionerServiceException ex)
            {
                _logger.LogError("No pensioner details found.");
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }
        [HttpGet("UserId/{userId}")]
        public async Task<ActionResult> GetPensionerIdById(string userId)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve pensioner Id by userId.");
                var result = await _pensionerRepository.GetPensionerIdById(userId);
                if (result == null)
                {
                    throw new PensionerServiceException("No pensioner id found for the given userID.");
                }
                _logger.LogInformation("Successfully retrieved pensioner Id by userId : {@result}", result);
                return Ok(result);
            }
            catch (PensionerServiceException ex)

            {
                _logger.LogError("No pensioner details found.");
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PensionerDetails>> AddPensionerDetails([FromBody] PensionerRequestDTO pensionerDetails)
        {
            try
            {
                _logger.LogInformation("Attempting to add pensioner details.");
                PensionerDetails request = _mapper.Map<PensionerDetails>(pensionerDetails);
                var result = await _pensionerRepository.AddPensionerDetails(request);
                _logger.LogInformation("Successfully added pensioner details. {@result}", result.PensionerId);
                return Ok(_mapper.Map<PensionResponseDTO>(result));

            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }

        }

        [HttpPut("{pensionerId}")]
        public async Task<IActionResult> UpdatePensionerDetailsById(Guid pensionerId, [FromBody] PensionerRequestDTO pensionerDetails)
        {
            try
            {
                _logger.LogInformation("Attempting to update pensioner details by pensioner Id.");
                var pensioner = await _pensionerRepository.GetPensionerDetailsById(pensionerId);
                if (pensioner == null)
                {
                    throw new PensionerServiceException("No pensioner details found for the given pensionerID.");

                }
                PensionerDetails request = _mapper.Map<PensionerDetails>(pensionerDetails);
                var result = await _pensionerRepository.UpdatePensionerDetailsById(pensionerId, request);
                _logger.LogInformation("Successfully updated pensioner details by Pensioner Id {@result}", result.PensionerId);
                return Ok(_mapper.Map<PensionResponseDTO>(result));

            }
            catch (PensionerServiceException ex)
            {
                _logger.LogError("No pensioner details found.");
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpDelete("{pensionerId}")]
        public async Task<IActionResult> DeletePensionerById(Guid pensionerId)
        {
            try
            {
                _logger.LogInformation("Attempting to delete pensioner details by pensioner Id.");
                var result = await _pensionerRepository.GetPensionerDetailsById(pensionerId);
                if (result == null)
                {
                    throw new PensionerServiceException("No pensioner details found for the given pensionerID.");
                }
                _pensionerRepository.DeletePensionerDetailsById(pensionerId);
                _logger.LogInformation("Successfully deleted pensioner details by Pensioner Id {@pensionerId}.", pensionerId);
                return NoContent();
            }
            catch (PensionerServiceException ex)
            {
                _logger.LogError("No pensioner details found.");
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                
                _logger.LogError("An unexpected error occurred while processing the request: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

    }
}
