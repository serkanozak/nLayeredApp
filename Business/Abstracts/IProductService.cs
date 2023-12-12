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
        Task<IPaginate<GetListedProductResponse>> GetListAsync(PageRequest pageRequest);
        Task<CreatedProductResponse> AddAsync(CreateProductRequest createProductRequest);
        Task<UpdatedProductResponse> UpdateAsync(UpdateProductRequest updateProductRequest);
        Task<DeletedProductResponse> DeleteAsync(DeleteProductRequest deleteProductRequest);
        
    }
}
