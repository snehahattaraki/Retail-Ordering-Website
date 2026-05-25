using System;
using System.Threading.Tasks;
using RetailOrdering.Application.DTOs.Common;
using RetailOrdering.Application.DTOs.Product;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductResponseDto> GetByIdAsync(Guid id);
        Task<PagedResultDto<ProductResponseDto>> SearchAsync(ProductSearchDto search);
        Task<ProductResponseDto> CreateAsync(CreateProductDto dto);
        Task<ProductResponseDto> UpdateAsync(Guid id, UpdateProductDto dto);
        Task DeleteAsync(Guid id);
    }
}
