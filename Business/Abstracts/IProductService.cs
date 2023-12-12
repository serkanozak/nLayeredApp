using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Dtos.Requests;
using Business.Dtos.Requests.ProductRequests;
using Business.Dtos.Responses.ProductResponses;
using Core.DataAccess.Paging;
using Entities.Concretes;

namespace Business.Abstracts
{
    public interface IProductService
    {
        Task<GetProductResponse> GetById(GetProductRequest getProductRequest);
        Task<IPaginate<GetListedProductResponse>> GetListAsync(PageRequest pageRequest);
        Task<CreatedProductResponse> AddAsync(CreateProductRequest createProductRequest);
        Task<UpdatedProductResponse> UpdateAsync(UpdateProductRequest updateProductRequest);
        Task<DeletedProductResponse> DeleteAsync(DeleteProductRequest deleteProductRequest);
        Task<IEnumerable<CreatedProductResponse>> AddRangeAsync(IEnumerable<CreateProductRequest> createProductRequests);
        Task<ICollection<UpdatedProductResponse>> UpdateRangeAsync(ICollection<UpdateProductRequest> updateProductRequests);
        Task<ICollection<DeletedProductResponse>> DeleteRangeAsync(ICollection<DeleteProductRequest> deleteProductRequests);
    }
}
