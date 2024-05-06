using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementBankingService.DTO;
using PensionManagementBankingService.Models;
using PensionManagementBankingService.Models.Repository.Implementation;
using PensionManagementBankingService.Models.Repository.Interfaces;

namespace PensionManagementBankingService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        private readonly IBankingRepository _bankingRepository;
        private readonly IMapper _mapper;

        public BankingController(IBankingRepository bankingRepository,IMapper mapper)
        {
            _bankingRepository = bankingRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankResponse>>> GetAllBankingDetails()
        {
            try
            {
                var bankingDetails = await _bankingRepository.GetAllBankingDetails();
                return Ok(bankingDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{bankId}")]
        public async Task<ActionResult<BankResponse>> GetBankingDetailsById(Guid bankId)
        {
            try
            {
                var bankingDetails = await _bankingRepository.GetBankingDetailsById(bankId);
                if(bankingDetails == null)
                {
                    return NotFound();
                }
                return Ok(bankingDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<BankRequest>> AddBankingDetails([FromBody]BankRequest bankingDetails)
        {
            try
            {
                
                var request=_mapper.Map<BankingDetails>(bankingDetails);
                var addedBankingDetails = await _bankingRepository.AddBankingDetails(request);
                var response = _mapper.Map<BankResponse>(addedBankingDetails);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{bankId}")]
        public async Task<ActionResult<BankRequest>> UpdateBankingDetailsById(Guid bankId, [FromBody] BankRequest bankingDetails)
        {
            try
            {
                
                var request = _mapper.Map<BankingDetails>(bankingDetails);
                var updatedBankingDetails = await _bankingRepository.UpdateBankingDetailsById(bankId, request);
                if(updatedBankingDetails == null)
                {
                    return NotFound();

                }
                return Ok(updatedBankingDetails);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{bankId}")]
        public IActionResult DeleteBankingDetailsById(Guid bankId)
        {
            try
            {
                _bankingRepository.DeleteBankingDetailsById(bankId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("PensionerId/{pensionerId}")]
        public async Task<ActionResult> GetPensionerIdById(Guid pensionerId)
        {
            try
            {
                var result = await _bankingRepository.GetBankDetailsByPensionerId(pensionerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
