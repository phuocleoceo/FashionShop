using AutoMapper;
using back_end.DTO;
using back_end.Models;

namespace back_end.Mapper
{
	public class FSMapping : Profile
	{
		public FSMapping()
		{
			CreateMap<Product, ProductDTO>()
				.ForMember(pDTO => pDTO.Category, prop => prop.MapFrom(p => p.Category.Name));
			CreateMap<ProductDTO, Product>();
			CreateMap<ProductUpsertDTO, Product>();
		}
	}
}