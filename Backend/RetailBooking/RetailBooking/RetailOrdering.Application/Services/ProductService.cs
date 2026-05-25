using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RetailOrdering.Application.DTOs.Common;
using RetailOrdering.Application.DTOs.Product;
using RetailOrdering.Application.Exceptions;
using RetailOrdering.Application.Interfaces.Repositories;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Domain.Entities.Product> _repo;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Domain.Entities.Product> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
        {
            var entity = _mapper.Map<Domain.Entities.Product>(dto);
            entity.Id = Guid.NewGuid();
            await _repo.AddAsync(entity);
            return _mapper.Map<ProductResponseDto>(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("Product not found");
            await _repo.DeleteAsync(entity);
        }

        public async Task<ProductResponseDto> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("Product not found");
            return _mapper.Map<ProductResponseDto>(entity);
        }

        public async Task<PagedResultDto<ProductResponseDto>> SearchAsync(ProductSearchDto search)
        {
            var items = await _repo.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(search.Query))
                items = items.Where(p => p.Name.Contains(search.Query, StringComparison.OrdinalIgnoreCase));
            if (search.BrandId != null)
                items = items.Where(p => p.BrandId == search.BrandId);
            if (search.CategoryId != null)
                items = items.Where(p => p.CategoryId == search.CategoryId);

            var total = items.Count();
            var paged = items.Skip((search.Page - 1) * search.PageSize).Take(search.PageSize).ToList();
            return new PagedResultDto<ProductResponseDto>
            {
                Page = search.Page,
                PageSize = search.PageSize,
                TotalCount = total,
                Items = paged.Select(p => _mapper.Map<ProductResponseDto>(p))
            };
        }

        public async Task<ProductResponseDto> UpdateAsync(Guid id, UpdateProductDto dto)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("Product not found");
            if (dto.Name != null) entity.Name = dto.Name;
            if (dto.Description != null) entity.Description = dto.Description;
            if (dto.Price.HasValue) entity.Price = dto.Price.Value;
            if (dto.Stock.HasValue) entity.Stock = dto.Stock.Value;
            await _repo.UpdateAsync(entity);
            return _mapper.Map<ProductResponseDto>(entity);
        }
    }
}
