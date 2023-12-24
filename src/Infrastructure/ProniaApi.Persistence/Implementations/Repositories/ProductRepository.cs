using ProniaApi.Application.Abstractions.Repositories;
using ProniaApi.Domain.Entities;
using ProniaApi.Persistence.Data;
using ProniaApi.Persistence.Implementations.Repositories.Generic;

namespace ProniaApi.Persistence.Implementations.Repositories
{
	public class ProductRepository:Repository<Product>,IProductRepository
	{
		public ProductRepository(AppDbContext context):base(context)
		{

		}
	}
}
