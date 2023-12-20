using Microsoft.Extensions.DependencyInjection;
using ProniaApi.Application.Abstractions.Repositories;
using ProniaApi.Application.Abstractions.Services;
using ProniaApi.Application.MappingProfiles;
using System.Reflection;

namespace ProniaApi.Application.ServiceRegistration
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddApplicationService(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			return services;
		}
	}
}
