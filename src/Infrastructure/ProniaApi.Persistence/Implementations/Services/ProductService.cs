using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaApi.Application.Abstractions.Repositories;
using ProniaApi.Application.Abstractions.Services;
using ProniaApi.Application.DTOs.Product;
using ProniaApi.Domain.Entities;

namespace ProniaApi.Persistence.Implementations.Services
{
	public class ProductService:IProductService
	{
		private readonly IProductRepository _repository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;

		public ProductService(IProductRepository repository, IMapper mapper, ICategoryRepository categoryRepository)
		{
			_repository = repository;
			_mapper = mapper;
			_categoryRepository = categoryRepository;
		}

		public async Task CreateAsync(CreateProductDto productDto)
		{
			var product = _mapper.Map<Product>(productDto);
			bool result = await _repository.IsExistAsync(p => p.Name==product.Name);
			if (result) throw new Exception("This product already exists");
			result = await _categoryRepository.IsExistAsync(c => c.Id == product.CategoryId);
			if(!result) throw new Exception("There is no such category");
			await _repository.AddAsync(product);
			try
			{
				await _repository.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.InnerException);
			}
			
		}

		public async Task<IEnumerable<GetProductDto>> GetAllPaginated(int page, int take)
		{
			IEnumerable<GetProductDto> dtos = _mapper.Map<IEnumerable<GetProductDto>>(await _repository.GetAllWhere(skip: (page - 1) * take, take: take, isTracking: false).ToArrayAsync());
			return dtos;
		}

		public async Task<ProductGetSingleDto> GetByIsAsync(int id)
		{
			Product product = await _repository.GetByIdAsync(id, includes: nameof(Product.Category));
			ProductGetSingleDto dto = _mapper.Map<ProductGetSingleDto>(product);
			return dto;
		}
	}
}
