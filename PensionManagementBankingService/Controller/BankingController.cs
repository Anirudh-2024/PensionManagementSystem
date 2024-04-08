using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementBankingService.DTO;
using PensionManagementBankingService.ExceptionHandling;
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
        public async Task<ActionResult<IEnumerable<BankResponse>>> GetAllBankingDetails()
        {
            try
            {
                var bankingDetails = await _bankingRepository.GetAllBankingDetails();
                return Ok(bankingDetails);
            }
            catch(EmptyResultException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An unexpected error occured,please try later");
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
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occured, please try later");
            }
        }
        [HttpPost]
        public async Task<ActionResult<BankRequest>> AddBankingDetails([FromBody]BankRequest bankingDetails)
        {
            try
            {
                BankingDetails bankDTO = new BankingDetails
                {
                    BankName = bankingDetails.BankName,
                    BranchName = bankingDetails.BranchName,
                    AccountNumber = bankingDetails.AccountNumber,
                    IfscCode = bankingDetails.IfscCode,
                    PanNumber = bankingDetails.PanNumber,
                    PensionerId = bankingDetails.PensionerId,

                };
                var addedBankingDetails = await _bankingRepository.AddBankingDetails(bankDTO);
                BankResponse bank = new BankResponse
                {
                    BankId = addedBankingDetails.BankId,
                    BankName = addedBankingDetails.BankName,
                    BranchName = addedBankingDetails.BranchName,
                    AccountNumber = addedBankingDetails.AccountNumber,
                    IfscCode = addedBankingDetails.IfscCode,
                    PanNumber = addedBankingDetails.PanNumber,
                    PensionerId = addedBankingDetails.PensionerId,
                };
                return Ok(bank);
            }
            catch(DuplicateRecordException ex)
            {
                return Conflict(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An unexpected error occured, please try later");
            }
        }
        [HttpPut("{bankId}")]
        public async Task<ActionResult<BankRequest>> UpdateBankingDetailsById(Guid bankId, [FromBody] BankRequest bankingDetails)
        {
            try
            {
                BankingDetails bankDTO = new BankingDetails
                {
                    BankName = bankingDetails.BankName,
                    BranchName = bankingDetails.BranchName,
                    AccountNumber = bankingDetails.AccountNumber,
                    IfscCode = bankingDetails.IfscCode,
                    PanNumber = bankingDetails.PanNumber,
                    PensionerId = bankingDetails.PensionerId,

                };
                var updatedBankingDetails = await _bankingRepository.UpdateBankingDetailsById(bankId, bankDTO);
                if(updatedBankingDetails == null)
                {
                    return NotFound();

                }
                return Ok(updatedBankingDetails);
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An unexpected error occured, please try later");
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
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occured, please try later");
            }
        }
        [HttpGet("GetBankIdByPensionerId")]
        public async Task<ActionResult> GetPensionerIdById(Guid pensionerId)
        {
            try
            {
                var result = await _bankingRepository.GetBankDetailsByPensionerId(pensionerId);
                return Ok(result);
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occured, please try later");
            }
        }
    }
}
