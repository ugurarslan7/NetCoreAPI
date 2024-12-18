using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Products;
using Services.Products.Create;
using Services.Products.Update;
using System.Net;

namespace Services.Products
{
    public class ProductService(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {

        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products = await repository.GetAll().ToListAsync();

            var productAsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productAsDto);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            var products = await repository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var productAsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productAsDto);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
        {
            var products = await repository.GetTopPriceProductAsync(count);

            var productAsDto = mapper.Map<List<ProductDto>>(products);

            return new ServiceResult<List<ProductDto>> { Data = productAsDto };
        }

        public async Task<ServiceResult<ProductDto>> GetByIdAsync(int id)
        {
            var product = await repository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult<ProductDto>.Fail("Product not found", HttpStatusCode.NotFound);

            }

            var productAsDto = mapper.Map<ProductDto>(product);

            return ServiceResult<ProductDto>.Success(productAsDto);
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
            var anyProduct = await repository.Where(p => p.Name == request.Name).AnyAsync();
            if (anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Fail("ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.NotFound);
            }


            var product =mapper.Map<Product>(request);

            await repository.AddAsync(product);

            await unitOfWork.SaveChangesAsyns();

            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        {
            //fast fail
            //guard clauses 

            var isProductNameExist = await repository.Where(p => p.Name == request.Name && p.Id != id).AnyAsync();
            if (isProductNameExist)
            {
                return ServiceResult.Fail("ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.BadRequest);
            }

            var product = mapper.Map<Product>(request);
            product.Id = id;

            repository.Update(product);

            await unitOfWork.SaveChangesAsyns();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(int id, int quantity)
        {
            var product = await repository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);

            }

            product.Stock = quantity;

            repository.Update(product);

            await unitOfWork.SaveChangesAsyns();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await repository.GetByIdAsync(id);

            repository.Delete(product!);

            await unitOfWork.SaveChangesAsyns();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }


    }


}
