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

			CreateMap<Category, CategoryDTO>();
			CreateMap<CategoryUpsertDTO, Category>();

			CreateMap<UserForRegistrationDTO, User>();

			CreateMap<ShoppingCart, ShoppingCartDTO>()
				.ForMember(scDTO => scDTO.Product, prop => prop.MapFrom(sc => sc.Product.Name))
				.ForMember(scDTO => scDTO.Price, prop => prop.MapFrom(sc => sc.Product.Price))
				.ForMember(scDTO => scDTO.ImagePath, prop => prop.MapFrom(sc => sc.Product.ImagePath));
			CreateMap<ShoppingCartAddDTO, ShoppingCart>();
		}
	}
}