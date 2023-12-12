using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Requests.CategoryRequests;
using Business.Dtos.Responses.CategoryResponses;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Concretes
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IMapper _mapper;
        public CategoryManager(ICategoryDal categoryDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
        }

        public async Task<CreatedCategoryResponse> AddAsync(CreateCategoryRequest createCategoryRequest)
        {
            Category addCategory = _mapper.Map<Category>(createCategoryRequest);
            addCategory.Id = Guid.NewGuid();
            Category createdCategory = await _categoryDal.AddAsync(addCategory);
            return _mapper.Map<CreatedCategoryResponse>(createdCategory);
        }

        public async Task<DeletedCategoryResponse> DeleteAsync(DeleteCategoryRequest deleteCategoryRequest)
        {
            Category deleteCategory = await _categoryDal.GetAsync(c => c.Id == deleteCategoryRequest.Id);
            await _categoryDal.DeleteAsync(deleteCategory);
            return _mapper.Map<DeletedCategoryResponse>(deleteCategory);
        }

        public async Task<IPaginate<GetListedCategoryResponse>> GetListAsync(PageRequest pageRequest)
        {
            var getList = await _categoryDal.GetListAsync(index:pageRequest.Index, size:pageRequest.Size);
            var result = _mapper.Map<Paginate<GetListedCategoryResponse>>(getList);
            return result;
        }

        public async Task<UpdatedCategoryResponse> UpdateAsync(UpdateCategoryRequest updateCategoryRequest)
        {
            Category updateCategory = await _categoryDal.GetAsync(c => c.Id == updateCategoryRequest.Id);
            _mapper.Map(updateCategoryRequest, updateCategory);
            Category updatedCategory = await _categoryDal.UpdateAsync(updateCategory);
            return _mapper.Map<UpdatedCategoryResponse>(updatedCategory);
        }
    }
}
