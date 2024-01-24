﻿using Microsoft.EntityFrameworkCore;
using PensionManagementPensionerService.Models.Context;
using PensionManagementPensionerService.Models.Repository.Interfaces;

namespace PensionManagementPensionerService.Models.Repository.Implementation
{
    public class PensionerRepository : IPensionerRepository
    {
        private readonly AppDbContext _appDbContext;

        public PensionerRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<PensionerDetails> AddPensionerDetails(PensionerDetails pensionerDetails)
        {
            var result = await _appDbContext.PensionerDetails.AddAsync(pensionerDetails);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PensionerDetails> DeletePensionerDetailsById(Guid pensionerId)
        {
            var result = await _appDbContext.PensionerDetails.FirstOrDefaultAsync(id => id.PensionerId == pensionerId);
            if (result != null)
            {
                _appDbContext.PensionerDetails.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<PensionerDetails>> GetAllPensionerDetails()
        { 
            return await _appDbContext.PensionerDetails.ToListAsync();
        }

        public async Task<PensionerDetails> GetPensionerDetailsById(Guid pensionerId)
        {
            return await _appDbContext.PensionerDetails.FirstOrDefaultAsync(id => id.PensionerId == pensionerId);
        }

        public async Task<PensionerDetails> UpdatePensionerDetails(PensionerDetails pensionerDetails)
        {
            var result = await _appDbContext.PensionerDetails.FirstOrDefaultAsync(id => id.PensionerId == pensionerDetails.PensionerId);
            if (result != null)
            {
                result.FullName = pensionerDetails.FullName;
                result.PhoneNumber = pensionerDetails.PhoneNumber;
                result.DateOfBirth = pensionerDetails.DateOfBirth;
                result.Gender = pensionerDetails.Gender;
                result.Age = pensionerDetails.Age;
                result.Address = pensionerDetails.Address;
                result.AadharNumber= pensionerDetails.AadharNumber;
                await _appDbContext.SaveChangesAsync();
                return result;

            }
            return null;
        }
    }
}
