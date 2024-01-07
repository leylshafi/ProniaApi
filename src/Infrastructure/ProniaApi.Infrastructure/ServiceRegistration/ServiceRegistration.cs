using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProniaApi.Application.Abstractions.Services;
using a=ProniaApi.Infrastructure.Implementations;
using System.Text;

namespace ProniaApi.Persistence.ServiceRegistration
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration config)
		{
			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(opt =>
			{
				opt.TokenValidationParameters = new()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = config["JWT:Issuer"],
					ValidAudience = config["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecurityKey"])),
					LifetimeValidator = (notBefore, expired, token, param) => token != null ? expired > DateTime.UtcNow : false
					
				};

			});
			services.AddAuthorization();
			services.AddScoped<ITokenHandler, a.TokenHandler>();
			return services;
		}
	}
}
