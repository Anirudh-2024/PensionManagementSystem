using AutoMapper;
using PensionManagementPensionerService.DTO;
using PensionManagementPensionerService.Models;

namespace PensionManagementPensionerService.AutoMapper
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<GuardianDetails, GuardianResponseDTO>().ReverseMap();
            CreateMap<GuardianDetails,GuardianRequestDTO>().ReverseMap();
            CreateMap<PensionerDetails, PensionerRequestDTO>().ReverseMap();
            CreateMap<PensionerDetails,PensionResponseDTO>().ReverseMap();
        }
    }
}
