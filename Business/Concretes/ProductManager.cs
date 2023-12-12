using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Requests.ProductRequests;
using Business.Dtos.Responses.ProductResponses;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Business.Concretes
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;

        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }

        public async Task<CreatedProductResponse> AddAsync(CreateProductRequest createProductRequest)
        {
            Product product = _mapper.Map<Product>(createProductRequest);
            product.Id = Guid.NewGuid();
            Product createdProduct = await _productDal.AddAsync(product);
            CreatedProductResponse createdProductResponse = _mapper.Map<CreatedProductResponse>(createdProduct);
            return createdProductResponse;
        }

        public async Task<UpdatedProductResponse> UpdateAsync(UpdateProductRequest updateProductRequest)
        {
            Product updateProduct = await _productDal.GetAsync(p => p.Id == updateProductRequest.Id);
            _mapper.Map(updateProductRequest, updateProduct);
            Product updatedProduct = await _productDal.UpdateAsync(updateProduct);
            UpdatedProductResponse updatedProductResponse = _mapper.Map<UpdatedProductResponse>(updatedProduct);
            return updatedProductResponse;
        }

        public async Task<DeletedProductResponse> DeleteAsync(DeleteProductRequest deleteProductRequest)
        {
            Product deleteProduct = await _productDal.GetAsync(p => p.Id == deleteProductRequest.Id);
            await _productDal.DeleteAsync(deleteProduct);
            DeletedProductResponse deletedProductResponse = _mapper.Map<DeletedProductResponse>(deleteProduct);
            return deletedProductResponse;
        }

        public async Task<IPaginate<GetListedProductResponse>> GetListAsync(PageRequest pageRequest)
        {
            var getList = await _productDal.GetListAsync(include: p => p.Include(p => p.Category),
                index: pageRequest.Index, size: pageRequest.Size);

            var result = _mapper.Map<Paginate<GetListedProductResponse>>(getList);
            return result;

            //List<GetListedProductResponse> getList = _mapper.Map<List<GetListedProductResponse>>(productList.Items);
            //Paginate<GetListedProductResponse> paginate = _mapper.Map<Paginate<GetListedProductResponse>>(productList);
            //paginate.Items = getList;
            //return paginate;
        }

        public async Task<IEnumerable<CreatedProductResponse>> AddRangeAsync(IEnumerable<CreateProductRequest> createProductRequests)
        {
            List<Product> products = createProductRequests.Select(request => _mapper.Map<Product>(request)).ToList();

            foreach (var product in products)
            {
                product.Id = Guid.NewGuid();
            }

            await _productDal.AddRangeAsync(products);

            IEnumerable<CreatedProductResponse> createdProductResponses = products.Select(p => _mapper.Map<CreatedProductResponse>(p));

            return createdProductResponses;
        }

        public async Task<ICollection<UpdatedProductResponse>> UpdateRangeAsync(ICollection<UpdateProductRequest> updateProductRequests)
        {
            ICollection<Product> entities = _mapper.Map<ICollection<Product>>(updateProductRequests);

            await _productDal.UpdateRangeAsync(entities);

            var updatedResponses = _mapper.Map<ICollection<UpdatedProductResponse>>(entities);

            return updatedResponses;
        }

        public async Task<ICollection<DeletedProductResponse>> DeleteRangeAsync(ICollection<DeleteProductRequest> deleteProductRequests)
        {
            List<Product> products = deleteProductRequests.Select(request => _mapper.Map<Product>(request)).ToList();

            // Silinecek ürünlerin Id gelecek.
            var productIds = products.Select(p => p.Id).ToList();

            // Ürünleri paginate olarak alıyoruz.
            var paginatedProducts = await _productDal.GetListAsync(p => productIds.Contains(p.Id));

            // Paginatetten sadece ürünleri çekiyoruz.
            var productsToDelete = paginatedProducts.Items.ToList();

            // Range e göre siliyoruz.
            await _productDal.DeleteRangeAsync(productsToDelete);

            var deletedResponses = _mapper.Map<ICollection<DeletedProductResponse>>(productsToDelete);

            return deletedResponses;
        }

        public async Task<GetProductResponse> GetById(GetProductRequest getProductRequest)
        {
            Product getProduct = await _productDal.GetAsync(p => p.Id == getProductRequest.Id);
            GetProductResponse productResponse = _mapper.Map<GetProductResponse>(getProduct);
            return productResponse;
        }
    }
}

//tobeto projesinde tüm crud operasyonları tüm nesneler için hazır olsun.















//---------------------------------------- ADD MANUEL ----------------------------------------
//Product product = new Product();
//product.Id = Guid.NewGuid();
//product.ProductName = createProductRequest.ProductName;
//product.QuantityPerUnit = createProductRequest.QuantityPerUnit;
//product.UnitPrice = createProductRequest.UnitPrice;
//product.UnitsInStock = createProductRequest.UnitsInStock;

//Product createdProduct = await _productDal.AddAsync(product);

//CreatedProductResponse createdProductResponse = new CreatedProductResponse();
//createdProductResponse.Id = createdProduct.Id;
//createdProductResponse.ProductName = createProductRequest.ProductName;
//createdProductResponse.QuantityPerUnit = createProductRequest.QuantityPerUnit;
//createdProductResponse.UnitPrice = createProductRequest.UnitPrice;
//createdProductResponse.UnitsInStock = createProductRequest.UnitsInStock;

//return createdProductResponse;

//---------------------------------------------------------------------------------------------

//---------------------------------------- GETLISTASYNC MANUEL --------------------------------

//var productList = await _productDal.GetListAsync();

//List<GetListedProductResponse> getList = new List<GetListedProductResponse>();

//foreach (var item in productList.Items)
//{
//    GetListedProductResponse getListedProductResponse = new GetListedProductResponse();
//    getListedProductResponse.Id = item.Id;
//    getListedProductResponse.ProductName = item.ProductName;
//    getListedProductResponse.UnitPrice = item.UnitPrice;
//    getListedProductResponse.QuantityPerUnit = item.QuantityPerUnit;
//    getListedProductResponse.UnitsInStock = item.UnitsInStock;
//    getList.Add(getListedProductResponse);
//}

//Paginate<GetListedProductResponse> paginate = new Paginate<GetListedProductResponse>();
//paginate.Pages = productList.Pages;
//paginate.Items = getList;
//paginate.Index = productList.Index;
//paginate.Size = productList.Size;
//paginate.Count = productList.Count;
//paginate.From = productList.From;

//return paginate;
//----------------------------------------------------------------------------------------------




//ICollection<Product> entitiesToDelete = _mapper.Map<ICollection<Product>>(deleteProductRequests);

//await _productDal.DeleteRangeAsync(entitiesToDelete);

//var deletedResponses = _mapper.Map<ICollection<DeletedProductResponse>>(entitiesToDelete);

//return deletedResponses;

//List<Product> products = deleteProductRequests.Select(request => _mapper.Map<Product>(request)).ToList();

//foreach (var product in products)
//{
//    await _productDal.GetAsync(p => p.Id == product.Id);
//}

//await _productDal.DeleteRangeAsync(products);

//var deletedResponses = _mapper.Map<ICollection<DeletedProductResponse>>(products);

//return deletedResponses;