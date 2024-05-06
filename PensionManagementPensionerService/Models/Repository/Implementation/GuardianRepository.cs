using Microsoft.EntityFrameworkCore;
using PensionManagementPensionerService.ExceptionalHandling;
using PensionManagementPensionerService.Models.Context;
using PensionManagementPensionerService.Models.Repository.Interfaces;

namespace PensionManagementPensionerService.Models.Repository.Implementation
{
    public class GuardianRepository : IGuardianRepository
    {
        private readonly AppDbContext _appDbContext;

        public GuardianRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<GuardianDetails> AddGuardian(GuardianDetails guardianDetails)
        {
            try
            {
                var existingRecord = await _appDbContext.GuardianDetails.FirstOrDefaultAsync(u => u.PensionerId == guardianDetails.PensionerId);
                if (existingRecord != null)
                {
                    throw new DuplicateRecordException("A duplicate record already exists with the same pensioner Id");
                }
                GuardianDetails addGuardian = new GuardianDetails
                {
                    GuardianId = Guid.NewGuid(),
                    GuardianName = guardianDetails.GuardianName,
                    DateOfBirth = guardianDetails.DateOfBirth,
                    Relation = guardianDetails.Relation,
                    Age = guardianDetails.Age,
                    Gender = guardianDetails.Gender,
                    PhoneNumber = guardianDetails.PhoneNumber,
                    PensionerId = guardianDetails.PensionerId,

                };
                var result = await _appDbContext.GuardianDetails.AddAsync(addGuardian);
                await _appDbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            


        }
        public void DeleteGuardianById(Guid guardianId)
        {
            try
            {
                var result = _appDbContext.GuardianDetails.FirstOrDefault(id => id.GuardianId == guardianId);
                if (result == null)
                {
                    throw new NotFoundException("Guardian Details not found for the provided guardian Id.");
                }
                _appDbContext.GuardianDetails.Remove(result);
                _appDbContext.SaveChanges();
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<GuardianDetails>> GetAllGuardianDetails()
        {
            try
            {
                var result = await _appDbContext.GuardianDetails.Include(o => o.PensionerDetails).Include(o => o.PensionerDetails.PensionPlanDetails).ToListAsync();
                if (result.Count == 0)
                {
                    throw new EmptyResultException("There are no guardian details available in the database.");
                }
                return result;
            }
          
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GuardianDetails> GetGuardianById(Guid guardianId)
        {
            try
            {
                var result = await _appDbContext.GuardianDetails.Include(o => o.PensionerDetails).Include(o => o.PensionerDetails.PensionPlanDetails).FirstOrDefaultAsync(id => id.GuardianId == guardianId);
                if (result == null)
                {
                    throw new NotFoundException("Guardian Details not found for the provided guardianId.");

                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GuardianDetails> UpdateGuardianById(Guid guardianId, GuardianDetails guardianDetails)
        {
            try
            {
                var result = await _appDbContext.GuardianDetails.FirstOrDefaultAsync(id => id.GuardianId == guardianId);
                if (result == null)
                {
                    throw new NotFoundException("Guardian Details not found for the provided guardian Id.");

                }
                    result.GuardianName = guardianDetails.GuardianName;
                    result.Relation = guardianDetails.Relation;
                    result.PhoneNumber = guardianDetails.PhoneNumber;
                    result.Age = guardianDetails.Age;
                    result.Gender = guardianDetails.Gender;
                    result.DateOfBirth = guardianDetails.DateOfBirth;
                    await _appDbContext.SaveChangesAsync();
                    return result;
            }
           
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Guid?> GetGuadianIdByPensionerId(Guid pensionerId)
        {
            try
            {
                var result = await _appDbContext.GuardianDetails.FirstOrDefaultAsync(g => g.PensionerId == pensionerId);
                if (result == null)
                {
                    throw new NotFoundException("Guardian Id is not found by this Pensioner Id ");
                }
                return result.GuardianId;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


