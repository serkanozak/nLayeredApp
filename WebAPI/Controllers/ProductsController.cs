using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Requests.ProductRequests;
using Entities.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            var result = await _productService.GetListAsync(pageRequest);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateProductRequest createProductRequest)
        {
            var result = await _productService.AddAsync(createProductRequest); 
            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateProductRequest updateProductRequest)
        {
            var result = await _productService.UpdateAsync(updateProductRequest);
            return Ok(result);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteProductRequest deleteProductRequest)
        {
            var result = await _productService.DeleteAsync(deleteProductRequest);
            return Ok(result);
        }

        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRangeAsync([FromBody] List<CreateProductRequest> createProductRequest)
        {
            var result = await _productService.AddRangeAsync(createProductRequest);
            return Ok(result);
        }

        [HttpPost("UpdateRange")]
        public async Task<IActionResult> UpdateRangeAsync([FromBody] List<UpdateProductRequest> updateProductRequests)
        {
            var result = await _productService.UpdateRangeAsync(updateProductRequests);
            return Ok(result);
        }

        [HttpPost("DeleteRange")]
        public async Task<IActionResult> DeleteRangeAsync([FromBody] List<DeleteProductRequest> deleteProductRequests)
        {
            var result = await _productService.DeleteRangeAsync(deleteProductRequests);
            return Ok(result);
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetById([FromBody] GetProductRequest getProductRequest)
        {
            var result = await _productService.GetById(getProductRequest);
            return Ok(result);
        }
    }
}
