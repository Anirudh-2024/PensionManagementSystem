using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public BankingController(IBankingRepository bankingRepository)
        {
            _bankingRepository = bankingRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankingDetails>>> GetAllBankingDetails()
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
        public async Task<ActionResult<BankingDetails>> GetBankingDetailsById(Guid bankId)
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
        public async Task<ActionResult<BankingDetails>> AddBankingDetails([FromBody]BankingDetails bankingDetails)
        {
            try
            {
                var addedBankingDetails = await _bankingRepository.AddBankingDetails(bankingDetails);
                return CreatedAtAction(nameof(GetBankingDetailsById), new { bankId = addedBankingDetails.BankId }, addedBankingDetails);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{bankId}")]
        public async Task<ActionResult<BankingDetails>> UpdateBankingDetailsById(Guid bankId, [FromBody] BankingDetails bankingDetails)
        {
            try
            {
                var updatedBankingDetails = await _bankingRepository.UpdateBankingDetailsById(bankId, bankingDetails);
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

    }
}
