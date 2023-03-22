using AutoMapper;
using ngay8.Models;
using Service.Application.DTOs;

namespace ngay8.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TableDTOs, GraphData>()
                .ForMember(dest => dest.GrossValue, opt => opt.MapFrom(src => src.GrossValue))
                .ForMember(dest => dest.NetValue, opt => opt.MapFrom(src => src.NetValue))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Date.Value.Year));
        }
    }
}
