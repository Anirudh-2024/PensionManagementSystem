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
        private readonly ILogger<BankingController> _logger;

        public BankingController(IBankingRepository bankingRepository, ILogger<BankingController> logger)
        {
            _bankingRepository = bankingRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankResponse>>> GetAllBankingDetails()
        {
            try
            {
                var bankingDetails = await _bankingRepository.GetAllBankingDetails();
                if(bankingDetails.Count() == 0)
                {
                    throw new BankingExceptions( "BankingDetails Not Found");

                }
                _logger.LogInformation("Retrieved all banking details successfully");
                return Ok(bankingDetails);
            }
            catch (BankingExceptions ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all banking details");
                return StatusCode(404,ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An expected error occured while retrieving all bank details");
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
                    _logger.LogWarning($"No banking details found for ID: {bankId}");
                   throw new BankingExceptions("Banking details not found for given Id");
                }
                _logger.LogInformation($"Retrieved BankingDetails for ID: {bankId}");
                return Ok(bankingDetails);
            }
            catch (BankingExceptions ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving banking details for given Id{bankId}");
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An expected error occured while retrieving bank details by Id");
                return StatusCode(500, "An unexpected error occured, please try later");
            }
        }
        [HttpPost]
        public async Task<ActionResult<BankRequest>> AddBankingDetails([FromBody]BankRequest bankingDetails)
        {
            try
            {
                var existingRecord = await _bankingRepository.GetBankDetailsByPensionerId(bankingDetails.PensionerId);
                if(existingRecord!=null)
                {
                    throw new BankingExceptions("Duplicate record already exist");
                }
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
                _logger.LogInformation("Banking details added successfully");
                return Ok(bank);
            }
            catch (BankingExceptions ex)
            {
                _logger.LogError(ex, "Banking exception occurred while adding banking details");
                return StatusCode(500,ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An expected error occured while adding bank details");
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
                    _logger.LogWarning($"No banking details found for ID: {bankId}");
                    throw new BankingExceptions("No banking Details found for given Id");

                }
                _logger.LogInformation($"Updated BankingDetails for ID: {bankId}");
                return Ok(updatedBankingDetails);
            }
            catch (BankingExceptions ex)
            {
                _logger.LogError(ex, $"Error occurred while updating banking details for given Id{bankId}");
                return StatusCode(404,ex.Message);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"An expected error occured while updating bank details for Id {bankId}");
                return StatusCode(500, "An unexpected error occured, please try later");
            }
        }
        [HttpDelete("{bankId}")]
        public async Task<IActionResult> DeleteBankingDetailsById(Guid bankId)
        {
            try
            {
                var existing = await _bankingRepository.GetBankingDetailsById(bankId);
                if(existing == null)
                {
                    throw new BankingExceptions("Banking Details not found for given BankId");
                }
                _bankingRepository.DeleteBankingDetailsById(bankId);
                
                _logger.LogInformation($"Deleted BankingDetails for ID: {bankId}");
                return Ok();
            }
            catch (BankingExceptions ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting banking details for given Id{bankId}");
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"An expected error occured while deleting bank details for Id {bankId}");
                return StatusCode(500, "An unexpected error occured, please try later");
            }
        }
        [HttpGet("GetBankIdByPensionerId")]
        public async Task<ActionResult> GetPensionerIdById(Guid pensionerId)
        {
            try
            {
                var result = await _bankingRepository.GetBankDetailsByPensionerId(pensionerId);
                if(result == null)
                {
                    throw new BankingExceptions("BankId not found for given Pensioner Id");
                }
                _logger.LogInformation($"retrieved BankDetails for PensionerID: {pensionerId}");
                return Ok(result);
            }
            catch(BankingExceptions ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving banking details for pensionerId{pensionerId}");
                return StatusCode(404,ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An expected error occured while retrieving bank details for PensionerId {pensionerId}");
                return StatusCode(500, "An unexpected error occured, please try later");
            }
        }
    }
}
