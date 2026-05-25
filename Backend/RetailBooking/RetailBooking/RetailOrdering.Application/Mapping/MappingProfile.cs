using AutoMapper;
using RetailOrdering.Application.DTOs.Product;
using RetailOrdering.Application.DTOs.User;

namespace RetailOrdering.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Product, ProductResponseDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name));

            CreateMap<CreateProductDto, Domain.Entities.Product>();
            CreateMap<Domain.Entities.User, UserResponseDto>()
                .ForMember(d => d.Role, o => o.MapFrom(s => s.Role.Name));
        }
    }
}
