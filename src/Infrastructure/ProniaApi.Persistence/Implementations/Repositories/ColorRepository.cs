using ProniaApi.Application.Abstractions.Repositories;
using ProniaApi.Domain.Entities;
using ProniaApi.Persistence.Data;
using ProniaApi.Persistence.Implementations.Repositories.Generic;

namespace ProniaApi.Persistence.Implementations.Repositories
{
	public class ColorRepository:Repository<Color>,IColorRepository
	{
		public ColorRepository(AppDbContext context):base(context) { }
	}
}
