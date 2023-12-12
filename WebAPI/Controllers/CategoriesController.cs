using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Requests.CategoryRequests;
using Business.Dtos.Requests.ProductRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            var result = await _categoryService.GetListAsync(pageRequest);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateCategoryRequest createCategoryRequest)
        {
            var result = await _categoryService.AddAsync(createCategoryRequest);
            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest updateCategoryRequest)
        {
            var result = await _categoryService.UpdateAsync(updateCategoryRequest);
            return Ok(result);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryRequest deleteCategoryRequest)
        {
            var result = await _categoryService.DeleteAsync(deleteCategoryRequest);
            return Ok(result);
        }

        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRangeAsync([FromBody] List<CreateCategoryRequest> createCategoryRequest)
        {
            var result = await _categoryService.AddRangeAsync(createCategoryRequest);
            return Ok(result);
        }

        [HttpPost("UpdateRange")]
        public async Task<IActionResult> UpdateRangeAsync([FromBody] List<UpdateCategoryRequest> updateCategoryRequests)
        {
            var result = await _categoryService.UpdateRangeAsync(updateCategoryRequests);
            return Ok(result);
        }

        [HttpPost("DeleteRange")]
        public async Task<IActionResult> DeleteRangeAsync([FromBody] List<DeleteCategoryRequest> deleteCategoryRequests)
        {
            var result = await _categoryService.DeleteRangeAsync(deleteCategoryRequests);
            return Ok(result);
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetById([FromBody] GetCategoryRequest getCategoryRequest)
        {
            var result = await _categoryService.GetById(getCategoryRequest);
            return Ok(result);
        }
    }
}
