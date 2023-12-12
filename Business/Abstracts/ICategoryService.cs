using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Dtos.Requests;
using Business.Dtos.Requests.CategoryRequests;
using Business.Dtos.Responses.CategoryResponses;
using Core.DataAccess.Paging;

namespace Business.Abstracts
{
    public interface ICategoryService
    {
        Task<IPaginate<GetListedCategoryResponse>> GetListAsync(PageRequest pageRequest);
        Task<CreatedCategoryResponse> AddAsync(CreateCategoryRequest createCategoryRequest);
        Task<UpdatedCategoryResponse> UpdateAsync(UpdateCategoryRequest updateCategoryRequest);
        Task<DeletedCategoryResponse> DeleteAsync(DeleteCategoryRequest deleteCategoryRequest);
    }
}
