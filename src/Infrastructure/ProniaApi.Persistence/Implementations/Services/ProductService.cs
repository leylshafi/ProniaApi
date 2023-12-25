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
		private readonly IColorRepository _colorRepository;
		private readonly IMapper _mapper;

		public ProductService(IProductRepository repository, IMapper mapper, ICategoryRepository categoryRepository, IColorRepository colorRepository)
		{
			_repository = repository;
			_mapper = mapper;
			_categoryRepository = categoryRepository;
			_colorRepository = colorRepository;
		}

		public async Task CreateAsync(CreateProductDto productDto)
		{
			if (!await _repository.IsExistAsync(p => p.Name == productDto.Name)) throw new Exception("Not found");
			if(!await _categoryRepository.IsExistAsync(c=>c.Id==productDto.CategoryId)) throw new Exception("There is no such category");
			Product product = _mapper.Map<Product>(productDto);
			product.ProductColors = new List<ProductColor>();
			foreach (var item in productDto.ColorIds)
			{
				if (!await _colorRepository.IsExistAsync(c => c.Id == item)) throw new Exception("There is no such color");
				product.ProductColors.Add(new ProductColor{ColorId = item});
			}
			await _repository.AddAsync(product);
			await _repository.SaveChangesAsync();
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
		public async Task UpdateAsync(int id, ProductUpdateDto productDto)
		{
			Product existed = await _repository.GetByIdAsync(id,includes:nameof(Product.ProductColors));
			if (existed is null) throw new Exception("Not found");
			if (productDto.CategoryId != existed.CategoryId)
				if (!await _categoryRepository.IsExistAsync(c => c.Id == productDto.CategoryId)) throw new Exception("There is no such category");
			
			existed = _mapper.Map(productDto,existed);
			existed.ProductColors = existed.ProductColors = existed.ProductColors.Where(pc => productDto.ColorIds.Any(ci => pc.ColorId == ci)).ToList();
			foreach (var cId in productDto.ColorIds)
			{
				if (!await _colorRepository.IsExistAsync(c => c.Id == cId)) throw new Exception("Not found");
				if (!existed.ProductColors.Any(pc => pc.ColorId == cId))
				{
					existed.ProductColors.Add(new ProductColor { ColorId = cId });
				}
			}
			_repository.Update(existed);
			await _repository.SaveChangesAsync();
		}
	}

}
