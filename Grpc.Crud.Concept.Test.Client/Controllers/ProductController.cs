using Grpc.Crud.Concept.Test;
using Grpc.Crud.Concept.Test.Client.Entities;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
namespace Admin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly GrpcChannel _channel;
        private readonly ProductServiceProto.ProductServiceProtoClient _client;
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            _channel = GrpcChannel.ForAddress(_configuration.GetValue<string>("GrpcSettings:ProductServiceUrl"));
            _client = new ProductServiceProto.ProductServiceProtoClient(_channel);
        }
        [HttpGet("getProductlist")]
        public async Task<Products> GetProductListAsync()
        {
            try
            {
                var response = await _client.GetProductListAsync(new Empty { });
                return response;
            }
            catch
            {
            }
            return null;
        }
        [HttpGet("getProductbyid")]
        public async Task<ProductDetail> GetProductByIdAsync(string Id)
        {
            try
            {
                var request = new GetProductDetailRequest
                {
                    ProductId = Id
                };
                var response = await _client.GetProductAsync(request);
                return response;
            }
            catch
            {
            }
            return null;
        }
        [HttpPost("addProduct")]
        public async Task<ProductDetail> AddProductAsync(Product product)
        {
            try
            {
                var ProductDetail = new ProductDetail
                {
                    Id = product.Id,
                    Name = product.Name,
                    Quantity = product.Quantity
                };
                var response = await _client.CreateProductAsync(new CreateProductDetailRequest()
                {
                    Product = ProductDetail
                });
                return response;
            }
            catch
            {
            }
            return null;
        }
        [HttpPut("updateProduct")]
        public async Task<ProductDetail> UpdateProductAsync(Product product)
        {
            try
            {
                var ProductDetail = new ProductDetail
                {
                    Id = product.Id,
                    Name = product.Name,
                    Quantity = product.Quantity
                };
                var response = await _client.UpdateProductAsync(new UpdateProductDetailRequest()
                {
                    Product = ProductDetail
                });
                return response;
            }
            catch
            {
            }
            return null;
        }
        [HttpDelete("deleteProduct")]
        public async Task<DeleteProductDetailResponse> DeleteProductAsync(string Id)
        {
            try
            {
                var response = await _client.DeleteProductAsync(new DeleteProductDetailRequest()
                {
                    ProductId = Id
                });
                return response;
            }
            catch
            {
            }
            return null;
        }
    }
}