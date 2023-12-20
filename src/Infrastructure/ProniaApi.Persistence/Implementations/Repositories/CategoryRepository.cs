using ProniaApi.Application.Abstractions.Repositories;
using ProniaApi.Domain.Entities;
using ProniaApi.Persistence.Data;
using ProniaApi.Persistence.Implementations.Repositories.Generic;

namespace ProniaApi.Persistence.Implementations.Repositories
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		public CategoryRepository(AppDbContext context) : base(context)
		{

		}
	}
}
