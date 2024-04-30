using AutoMapper;
using PensionManagementBankingService.DTO;
using PensionManagementBankingService.Models;


namespace PensionManagementBankingService.AutoMapper
{
    public class BankingMapping:Profile
    {
        public BankingMapping()
        {
            CreateMap<BankRequest, BankingDetails>().ReverseMap();
            CreateMap<BankResponse, BankingDetails>().ReverseMap();
        }
        




    }
}
