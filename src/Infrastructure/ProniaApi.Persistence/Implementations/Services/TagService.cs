using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaApi.Application.Abstractions.Repositories;
using ProniaApi.Application.Abstractions.Services;
using ProniaApi.Application.DTOs.Category;
using ProniaApi.Application.DTOs.Tag;
using ProniaApi.Domain.Entities;

namespace ProniaApi.Persistence.Implementations.Services
{
	public class TagService : ITagService
	{
		private readonly ITagRepository _repository;
		private readonly IMapper _mapper;
		public TagService(ITagRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task CreateAsync(CreateTagDto tagDto)
		{
			Tag tag = _mapper.Map<Tag>(tagDto);
			await _repository.AddAsync(tag);
			await _repository.SaveChangesAsync();
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<ICollection<GetTagDto>> GetAllAsync(int page, int take)
		{
			var tags = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, isTracking: false).ToListAsync();
			var result = _mapper.Map<ICollection<GetTagDto>>(tags);
			return result;
		}

		public Task<GetTagDto> GetAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateAsync(int id, UpdateTagDto tagDto)
		{
			Tag tag = await _repository.GetByIdAsync(id);
			if (tag is null) throw new Exception("Not found");
			tag.Name = tagDto.Name;
			_repository.Update(tag);
			await _repository.SaveChangesAsync();
		}
	}
}
