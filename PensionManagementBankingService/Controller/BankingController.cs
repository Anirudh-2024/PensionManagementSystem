using AutoMapper;
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
            catch (BankingExceptions ex)
            {
                switch (ex.Type)
                {
                    case BankingExceptions.ErrorType.DuplicateRecord:
                        return Conflict(ex.Message);
                    case BankingExceptions.ErrorType.EmptyResult:
                        return NotFound(ex.Message);
                    case BankingExceptions.ErrorType.NotFound:
                        return NotFound(ex.Message);
                    default:
                        throw;
                }
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
            catch (BankingExceptions ex)
            {
                switch (ex.Type)
                {
                    case BankingExceptions.ErrorType.DuplicateRecord:
                        return Conflict(ex.Message);
                    case BankingExceptions.ErrorType.EmptyResult:
                        return NotFound(ex.Message);
                    case BankingExceptions.ErrorType.NotFound:
                        return NotFound(ex.Message);
                    default:
                        throw;
                }
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
                
                var request=_mapper.Map<BankingDetails>(bankingDetails);
                var addedBankingDetails = await _bankingRepository.AddBankingDetails(request);
                var response = _mapper.Map<BankResponse>(addedBankingDetails);
                return Ok(response);
            }
            catch (BankingExceptions ex)
            {
                switch (ex.Type)
                {
                    case BankingExceptions.ErrorType.DuplicateRecord:
                        return Conflict(ex.Message);
                    case BankingExceptions.ErrorType.EmptyResult:
                        return NotFound(ex.Message);
                    case BankingExceptions.ErrorType.NotFound:
                        return NotFound(ex.Message);
                    default:
                        throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occured, please try later");
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
            catch (BankingExceptions ex)
            {
                switch (ex.Type)
                {
                    case BankingExceptions.ErrorType.DuplicateRecord:
                        return Conflict(ex.Message);
                    case BankingExceptions.ErrorType.EmptyResult:
                        return NotFound(ex.Message);
                    case BankingExceptions.ErrorType.NotFound:
                        return NotFound(ex.Message);
                    default:
                        throw;
                }
            }

            catch (Exception ex)
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
            catch (BankingExceptions ex)
            {
                switch (ex.Type)
                {
                    case BankingExceptions.ErrorType.DuplicateRecord:
                        return Conflict(ex.Message);
                    case BankingExceptions.ErrorType.EmptyResult:
                        return NotFound(ex.Message);
                    case BankingExceptions.ErrorType.NotFound:
                        return NotFound(ex.Message);
                    default:
                        throw;
                }
            }

            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occured, please try later");
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
            catch(BankingExceptions ex)
            {
                switch(ex.Type)
                {
                    case BankingExceptions.ErrorType.DuplicateRecord:
                        return Conflict(ex.Message);
                    case BankingExceptions.ErrorType.EmptyResult:
                        return NotFound(ex.Message);
                    case BankingExceptions.ErrorType.NotFound:
                        return NotFound(ex.Message);
                    default:
                        throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occured, please try later");
            }
        }
    }
}
