using AutoMapper;
using Service.Application.DTOs;
using Service.Domain.Models;

namespace Service.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransactionData, TableDTOs>()
                .ForMember(des => des.Gcedata, op => op.MapFrom(s => s.TransactionGcedata))
                .ForMember(des => des.Gcedata, op => op.MapFrom(s => s.TransactionGcrdatas));
        }
    }
}
