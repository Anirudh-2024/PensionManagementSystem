using Microsoft.EntityFrameworkCore;
using PensionManagementPensionerService.ExceptionalHandling;
using PensionManagementPensionerService.Models.Context;
using PensionManagementPensionerService.Models.Repository.Interfaces;

namespace PensionManagementPensionerService.Models.Repository.Implementation
{
    public class PensionerRepository : IPensionerRepository
    {
        private readonly AppDbContext _appDbContext;

        public PensionerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PensionerDetails> AddPensionerDetails(PensionerDetails pensionerDetails)
        {
            try
            {
                var existingRecord = await _appDbContext.PensionerDetails.FirstOrDefaultAsync(p => p.Id == pensionerDetails.Id);
                if (existingRecord != null)
                {
                    throw new DuplicateRecordException("A duplicate record already exists with the same user Id");
                }
                PensionerDetails addPensionerDetails = new PensionerDetails
                {
                    PensionerId = Guid.NewGuid(),
                    FullName = pensionerDetails.FullName,
                    DateOfBirth = pensionerDetails.DateOfBirth,
                    Age = pensionerDetails.Age,
                    Gender = pensionerDetails.Gender,
                    Address = pensionerDetails.Address,
                    PensionPlanId = pensionerDetails.PensionPlanId,
                    Id = pensionerDetails.Id,
                    AadharNumber = pensionerDetails.AadharNumber,
                    PhoneNumber = pensionerDetails.PhoneNumber,

                };
                var result = await _appDbContext.PensionerDetails.AddAsync(addPensionerDetails);
                await _appDbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public void DeletePensionerDetailsById(Guid pensionerId)
        {
            try
            {
                
                var result = _appDbContext.PensionerDetails.FirstOrDefault(id => id.PensionerId == pensionerId);
                if (result == null)
                {
                    throw new NotFoundException("Pensioner Details not found for the provided Id.");
                }
                _appDbContext.PensionerDetails.Remove(result);
                _appDbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<PensionerDetails>> GetAllPensionerDetails()
        {
            try
            {
                var result =  await _appDbContext.PensionerDetails.Include(o => o.PensionPlanDetails).ToListAsync();
                if (result.Count == 0)
                {
                    throw new EmptyResultException("There are no pensioner details available in the database.");
                }
                return result;
            }
          
             catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PensionerDetails> GetPensionerDetailsById(Guid pensionerId)
        {
            try
            {

                var result = await _appDbContext.PensionerDetails.Include(o => o.PensionPlanDetails).FirstOrDefaultAsync(id => id.PensionerId == pensionerId);
                if (result == null)
                {
                    throw new NotFoundException("Pensioner Details not found for the provided Id.");

                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<PensionerDetails> UpdatePensionerDetailsById(Guid pensionerId, PensionerDetails pensionerDetails)
        {
            try
            {
                var result = await _appDbContext.PensionerDetails.Include(o => o.PensionPlanDetails).FirstOrDefaultAsync(id => id.PensionerId == pensionerId);
                if (result == null)
                {
                    throw new NotFoundException("Pensioner Details not found for the provided Id.");

                }
                result.FullName = pensionerDetails.FullName;
                result.PhoneNumber = pensionerDetails.PhoneNumber;
                result.DateOfBirth = pensionerDetails.DateOfBirth;
                result.Gender = pensionerDetails.Gender;
                result.Age = pensionerDetails.Age;
                result.Address = pensionerDetails.Address;
                result.AadharNumber = pensionerDetails.AadharNumber;
                await _appDbContext.SaveChangesAsync();
                return result;
            }
           catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Guid?> GetPensionerIdById(string userId)
        {
            try
            {
                var pensioner = await _appDbContext.PensionerDetails.FirstOrDefaultAsync(p => p.Id == userId);
                if (pensioner == null)
                {
                    throw new NotFoundException("Pensioner Id is not found by this UserId ");
                }
                return pensioner.PensionerId;

            }
            catch (Exception ex)
            {
                throw ex;
            }
                
        }
    }
}
