using Microsoft.AspNetCore.Mvc;
using Repositories.Products;
using Services.Filters;
using Services.Products;
using Services.Products.Create;
using Services.Products.Update;

namespace API.Controllers
{
    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await productService.GetAllListAsync());
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize)
        {
            return CreateActionResult(await productService.GetPagedAllListAsync(pageNumber,pageSize));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await productService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest createProductRequest)
        {
            return CreateActionResult(await productService.CreateAsync(createProductRequest));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product, int>))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest updateProductRequest) =>
            CreateActionResult(await productService.UpdateAsync(id, updateProductRequest));

        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStock(int id, int quantity) =>
            CreateActionResult(await productService.UpdateStockAsync(id,quantity));

        [ServiceFilter(typeof(NotFoundFilter<Product,int>))]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
            CreateActionResult(await productService.DeleteAsync(id));
    }
}
