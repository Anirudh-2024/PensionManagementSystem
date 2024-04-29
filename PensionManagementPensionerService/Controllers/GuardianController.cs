using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using PensionManagementPensionerService.Models;
using PensionManagementPensionerService.DTO;
using PensionManagementPensionerService.Models.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;

namespace PensionManagementPensionerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardianController : ControllerBase
    {
        private readonly IGuardianRepository _guardianRepository;
        private readonly IMapper _mapper;

        public GuardianController(IGuardianRepository guardianRepository, IMapper mapper)
        {
            _guardianRepository = guardianRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllGuardianDetails")]
        public async Task<ActionResult<IEnumerable<GuardianResponseDTO>>> GetAllGuardianDetails()
        {
            try
            {
                var result = await _guardianRepository.GetAllGuardianDetails();
                return Ok(_mapper.Map<List<GuardianResponseDTO>>(result));
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
                return Ok(_mapper.Map<GuardianResponseDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddGuardianDetails")]
        public async Task<ActionResult<GuardianDetails>> AddGuardian([FromBody] GuardianRequestDTO guardianDetails)
        {
            try
            {
                GuardianDetails request = _mapper.Map<GuardianDetails>(guardianDetails);
                var result = await _guardianRepository.AddGuardian(request);
                return Ok(_mapper.Map<GuardianResponseDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("UpdateGuardianById")]
        public async Task<IActionResult> UpdateGuardianById(Guid guardianId, [FromBody] GuardianRequestDTO guardianDetails)
        {
            try
            {
                GuardianDetails request = _mapper.Map<GuardianDetails>(guardianDetails);
                var result = await _guardianRepository.UpdateGuardianById(guardianId, request);
                return Ok(_mapper.Map<GuardianResponseDTO>(result));
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

        [HttpGet ("GetGuardianIdByPensionerId")]
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
