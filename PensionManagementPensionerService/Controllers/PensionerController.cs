using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementPensionerService.Models.Repository.Interfaces;
using PensionManagementPensionerService.Models;
using PensionManagementPensionerService.DTO;
using AutoMapper;

namespace PensionManagementPensionerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerController : ControllerBase
    {
        private readonly IPensionerRepository _pensionerRepository;
        private readonly IMapper _mapper;

        public PensionerController(IPensionerRepository pensionerRepository, IMapper mapper)
        {
            _pensionerRepository = pensionerRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllPensionerDetails")]
        public async Task<ActionResult<IEnumerable<PensionResponseDTO>>> GetAllPensionerDetails()
        {
            try
            {
                var result = await _pensionerRepository.GetAllPensionerDetails();
                return Ok(_mapper.Map<List<PensionResponseDTO>>(result));
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
                return Ok(_mapper.Map<PensionResponseDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddPensionerDetails")]
        public async Task<ActionResult<PensionerDetails>> AddPensionerDetails([FromBody] PensionerRequestDTO pensionerDetails)
        {
            try
            {
                PensionerDetails request = _mapper.Map<PensionerDetails>(pensionerDetails);
                var result = await _pensionerRepository.AddPensionerDetails(request);
                return Ok(_mapper.Map<PensionResponseDTO>(result));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("UpdatePensionerDetailsById")]
        public async Task<IActionResult> UpdatePensionerDetailsById(Guid pensionerId, [FromBody] PensionerRequestDTO pensionerDetails)
        {
            try
            {
                PensionerDetails request = _mapper.Map<PensionerDetails>(pensionerDetails);
                var result = await _pensionerRepository.UpdatePensionerDetailsById(pensionerId, request);
                return Ok(_mapper.Map<PensionResponseDTO>(result));
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
