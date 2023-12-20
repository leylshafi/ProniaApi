using AutoMapper;
using ProniaApi.Application.DTOs.Tag;
using ProniaApi.Domain.Entities;

namespace ProniaApi.Application.MappingProfiles
{
	public class TagProfile:Profile
	{
		public TagProfile()
		{
			CreateMap<Tag, GetTagDto>();
			CreateMap<CreateTagDto, Tag>();

		}
	}
}
