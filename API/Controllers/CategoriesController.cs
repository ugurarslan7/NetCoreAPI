using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Categories;
using Services.Categories.Create;
using Services.Products;
using Services.Products.Create;
using Services.Products.Update;

namespace API.Controllers
{
    public class CategoriesController(ICategoryService categoryService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return CreateActionResult(await categoryService.GetAllListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategories(int id)
        {
            return CreateActionResult(await categoryService.GetByIdAsync(id));
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetCategoryWithProducts()
        {
            return CreateActionResult(await categoryService.GetCategoryWithProductsAsync());
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCategoryWithProducts(int id)
        {
            return CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest createCategoryRequest)
        {
            return CreateActionResult(await categoryService.CreateAsync(createCategoryRequest));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryRequest updateCategoryRequest) =>
      CreateActionResult(await categoryService.UpdateAsync(id, updateCategoryRequest));


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
            CreateActionResult(await categoryService.DeleteAsync(id));
    }
}
