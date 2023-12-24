using AutoMapper;
using ProniaApi.Application.DTOs.Product;
using ProniaApi.Domain.Entities;

namespace ProniaApi.Application.MappingProfiles
{
	public class ProductProfile:Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, GetProductDto>().ReverseMap();
			CreateMap<Product, ProductGetSingleDto>().ReverseMap();
			CreateMap<CreateProductDto, Product>();
		}
	}
}
