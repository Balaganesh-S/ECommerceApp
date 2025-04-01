using AutoMapper;
using ECommerceApp.Domain;
using ECommerceApp.DTO;
using ECommerceApp.Repositories;
using System.Net;

namespace ECommerceApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper) {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<ResponseDto<ProductResponseDto>> CreateProductAsync(int categoryId, ProductRequestDto productRequestDto)
        {   
            Product productRequest = mapper.Map<Product>(productRequestDto);
            Product product = await productRepository.CreateProductAsync(categoryId, productRequest);
            return new ResponseDto<ProductResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully Created!",
                Data = mapper.Map<ProductResponseDto>(product)
            };
        }

        public async Task<ResponseDto<ProductResponseDto>> DeleteProductAsync(int id)
        {
            Product product = await productRepository.DeleteProductAsync(id);
            return new ResponseDto<ProductResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully Deleted!",
                Data = mapper.Map<ProductResponseDto>(product)
            };
        }

        public async  Task<ResponseDto<ProductResponseDto>> GetProductByIdAsync(int id)
        {
            Product product = await productRepository.GetProductByIdAsync(id);
            return new ResponseDto<ProductResponseDto> {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully Fetched!",
                Data = mapper.Map<ProductResponseDto>(product)
            };
        }

        public async Task<ResponseDto<List<ProductResponseDto>>> GetProductByKeywordAsync(string keyword)
        {
            List<Product> products = await productRepository.GetProductByKeywordAsync(keyword);
            return new ResponseDto<List<ProductResponseDto>>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully Fetched!",
                Data = mapper.Map<List<ProductResponseDto>>(products)
            };

        }

        public async Task<ResponseDto<List<ProductResponseDto>>> GetProductsAsync()
        {
            List<Product> products = await productRepository.GetProductsAsync();
            return new ResponseDto<List<ProductResponseDto>> { 
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully Fetched!",
                Data = mapper.Map<List<ProductResponseDto>>(products)
            };
        }

        public async Task<ResponseDto<List<ProductResponseDto>>> GetProductsByCategoryAsync(int categoryId)
        {
            List<Product> products = await productRepository.GetProductsByCategoryAsync(categoryId);

            return new ResponseDto<List<ProductResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully Fetched!",
                Data = mapper.Map<List<ProductResponseDto>>(products)
            };
        }

        public async Task<ResponseDto<ProductResponseDto>> UpdateProductAsync(int productId, ProductRequestDto productRequestDto)
        {
            Product productRequest = mapper.Map<Product>(productRequestDto);
            Product product = await productRepository.UpdateProductAsync(productId, productRequest);
            return new ResponseDto<ProductResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully Updated!",
                Data = mapper.Map<ProductResponseDto>(product)
            };
        }

        public async Task<ResponseDto<int>> GetProductCountAsync()
        {
            int count = await productRepository.GetProductCountAsync();
            return new ResponseDto<int>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Sucessfully fetched!",
                Data = count
            };
        }
    }
}
