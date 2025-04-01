using ECommerceApp.Domain;
using ECommerceApp.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Services
{
    public interface IProductService
    {
        Task<ResponseDto<List<ProductResponseDto>>> GetProductsAsync();
        Task<ResponseDto<ProductResponseDto>> GetProductByIdAsync(int id);
        Task<ResponseDto<ProductResponseDto>> CreateProductAsync(int categoryId, ProductRequestDto productRequestDto);
        Task<ResponseDto<ProductResponseDto>> UpdateProductAsync(int productId, ProductRequestDto productRequestDto);
        Task<ResponseDto<List<ProductResponseDto>>> GetProductsByCategoryAsync(int categoryId);
        Task<ResponseDto<ProductResponseDto>> DeleteProductAsync(int id);
        Task<ResponseDto<List<ProductResponseDto>>> GetProductByKeywordAsync(string keyword);
        Task<ResponseDto<int>> GetProductCountAsync();


    }
}
