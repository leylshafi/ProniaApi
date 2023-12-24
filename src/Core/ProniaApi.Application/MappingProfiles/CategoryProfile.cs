using AutoMapper;
using ProniaApi.Application.DTOs.Category;
using ProniaApi.Domain.Entities;

namespace ProniaApi.Application.MappingProfiles
{
	public class CategoryProfile:Profile
	{
		public CategoryProfile()
		{
			CreateMap<Category, GetCategoryDto>().ReverseMap();
			CreateMap<CreateCategoryDto, Category>();
			CreateMap<Category, IncludeCategoryDto>();
		}
	}
}
