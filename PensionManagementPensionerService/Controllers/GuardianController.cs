﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using PensionManagementPensionerService.Models;
using PensionManagementPensionerService.DTO;
using PensionManagementPensionerService.Models.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using PensionManagementPensionerService.ExceptionalHandling;

namespace PensionManagementPensionerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardianController : ControllerBase
    {
        private readonly IGuardianRepository _guardianRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GuardianController> _logger;

        public GuardianController(IGuardianRepository guardianRepository, ILogger<GuardianController> logger, IMapper mapper)
        {
            _guardianRepository = guardianRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuardianDetails>>> GetAllGuardianDetails()
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve all guardian details.");
                var result = await _guardianRepository.GetAllGuardianDetails();
                if (result.Count() == 0)
                {
                    throw new PensionerServiceException("No guardian details found.");
                }
                
                _logger.LogInformation("Successfully retrieved all guardian details");
                return Ok(_mapper.Map<List<GuardianResponseDTO>>(result));
            }
            catch (PensionerServiceException ex)
            {
                _logger.LogError("Empty result returned while retrieving guardian details");
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpGet("{guardianId}")]
        public async Task<IActionResult> GetGuardianDetailsById(Guid guardianId)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve guardian details by guardian Id.");
                var result = await _guardianRepository.GetGuardianById(guardianId);
                if(result == null)
                {
                    throw new PensionerServiceException("No guardian details found for the given guardianID.");
                }
                _logger.LogInformation("Successfully retrieved guardian details by guardian Id: {@result}", result.GuardianId);
                return Ok(_mapper.Map<GuardianResponseDTO>(result));
            }
            catch (PensionerServiceException ex)
            {
                _logger.LogError("No guardian details found.");
                return StatusCode(404, ex.Message);
                
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<GuardianDetails>> AddGuardian([FromBody] GuardianRequestDTO guardianDetails)
        {
            try
            {
                _logger.LogInformation("Attempting to add guardian details.");
                GuardianDetails request = _mapper.Map<GuardianDetails>(guardianDetails);
                var result = await _guardianRepository.AddGuardian(request);
                _logger.LogInformation("Successfully added guardian details : {@result}", result.GuardianId);
                return Ok(_mapper.Map<GuardianResponseDTO>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }

        }

        [HttpPut("{guardianId}")]
        public async Task<IActionResult> UpdateGuardianById(Guid guardianId, [FromBody] GuardianRequestDTO guardianDetails)
        {
            try
            {
                _logger.LogInformation("Attempting to update guardian details by guardian Id.");
                var guardian = await _guardianRepository.GetGuardianById(guardianId);
                if (guardian == null)
                {
                    throw new PensionerServiceException("No guardian details found for the given guardianID.");

                }
                GuardianDetails request = _mapper.Map<GuardianDetails>(guardianDetails);
                var result = await _guardianRepository.UpdateGuardianById(guardianId, request);
                _logger.LogInformation("Successfully updated guardian details by guardian Id {@result}", result.GuardianId);
                 return Ok(_mapper.Map<GuardianResponseDTO>(result));
            }
            catch (PensionerServiceException ex)
            {
                _logger.LogError("No guardian details found.");
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpDelete("{guardianId}")] 
        public async Task<IActionResult> DeleteGuardianById(Guid guardianId)
        {
            try
            {
                _logger.LogInformation("Attempting to delete guardian details by guardian Id.");
                var result = await _guardianRepository.GetGuardianById(guardianId);
                if (result == null)
                {
                    throw new PensionerServiceException("No guardian details found for the given guardianID.");
                }
                _guardianRepository.DeleteGuardianById(guardianId);
                _logger.LogInformation("Successfully deleted guardian details {@guardianId}", guardianId);
                return NoContent();
            }
            catch (PensionerServiceException ex)
            {
                _logger.LogError("No guardian details found.");
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
        }

        [HttpGet ("PensionerId/{pensionerId}")]
        public async Task<ActionResult> GetGuardianIdByPensionerId(Guid pensionerId)
        {
            try
            { 
                _logger.LogInformation("Attempting to retrieve guardianId by pensioner Id.");
                var result = await _guardianRepository.GetGuadianIdByPensionerId(pensionerId);
                if (result == null)
                {
                    throw new PensionerServiceException("No guardianId found for the given pensionerID.");
                }
                _logger.LogInformation("Successfully retrieved guardianId by pensioner Id: {@result}", result);
                return Ok(result);
            }

            catch (PensionerServiceException ex)
            {
                _logger.LogError("No guardianId found for the given pensionerID.");
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while processing the request: {@ErrorMessage}", ex.Message);
                return StatusCode(500, "An unexpected error occurred while processing the request. Please try again later.");
            }
            
        }
    }
}
