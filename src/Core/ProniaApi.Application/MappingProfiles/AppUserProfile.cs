using AutoMapper;
using ProniaApi.Application.DTOs.Users;
using ProniaApi.Domain.Entities;

namespace ProniaApi.Application.MappingProfiles
{
	public class AppUserProfile:Profile
	{
		public AppUserProfile()
		{
			CreateMap<RegisterDto, AppUser>().ReverseMap();
		}
	}
}
