using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Requests.CategoryRequests;
using Business.Dtos.Requests.ProductRequests;
using Business.Dtos.Responses.CategoryResponses;
using Business.Dtos.Responses.ProductResponses;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using DataAccess.Concretes;
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

        public async Task<IEnumerable<CreatedCategoryResponse>> AddRangeAsync(IEnumerable<CreateCategoryRequest> createCategoryRequests)
        {
            List<Category> categories = createCategoryRequests.Select(request => _mapper.Map<Category>(request)).ToList();

            foreach (var category in categories)
            {
                category.Id = Guid.NewGuid();
            }

            await _categoryDal.AddRangeAsync(categories);

            IEnumerable<CreatedCategoryResponse> createdCategoryResponses = categories.Select(p => _mapper.Map<CreatedCategoryResponse>(p));

            return createdCategoryResponses;
        }

        public async Task<DeletedCategoryResponse> DeleteAsync(DeleteCategoryRequest deleteCategoryRequest)
        {
            Category deleteCategory = await _categoryDal.GetAsync(c => c.Id == deleteCategoryRequest.Id);
            await _categoryDal.DeleteAsync(deleteCategory);
            return _mapper.Map<DeletedCategoryResponse>(deleteCategory);
        }

        public async Task<ICollection<DeletedCategoryResponse>> DeleteRangeAsync(ICollection<DeleteCategoryRequest> deleteCategoryRequests)
        {
            List<Category> categories = deleteCategoryRequests.Select(request => _mapper.Map<Category>(request)).ToList();

            // Silinecek ürünlerin Id gelecek.
            var categoryIds = categories.Select(p => p.Id).ToList();

            // Ürünleri paginate olarak alıyoruz.
            var paginatedCategories = await _categoryDal.GetListAsync(p => categoryIds.Contains(p.Id));

            // Paginatetten sadece ürünleri çekiyoruz.
            var categoriesToDelete = paginatedCategories.Items.ToList();

            // Range e göre siliyoruz.
            await _categoryDal.DeleteRangeAsync(categoriesToDelete);

            var deletedResponses = _mapper.Map<ICollection<DeletedCategoryResponse>>(categoriesToDelete);

            return deletedResponses;
        }

        public async Task<GetCategoryResponse> GetById(GetCategoryRequest getCategoryRequest)
        {
            Category getCategory = await _categoryDal.GetAsync(c => c.Id == getCategoryRequest.Id);
            GetCategoryResponse categoryResponse = _mapper.Map<GetCategoryResponse>(getCategory);
            return categoryResponse;
        }

        public async Task<IPaginate<GetListedCategoryResponse>> GetListAsync(PageRequest pageRequest)
        {
            var getList = await _categoryDal.GetListAsync(index: pageRequest.Index, size: pageRequest.Size);
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

        public async Task<ICollection<UpdatedCategoryResponse>> UpdateRangeAsync(ICollection<UpdateCategoryRequest> updateCategoryRequests)
        {
            ICollection<Category> entities = _mapper.Map<ICollection<Category>>(updateCategoryRequests);

            await _categoryDal.UpdateRangeAsync(entities);

            var updatedResponses = _mapper.Map<ICollection<UpdatedCategoryResponse>>(entities);

            return updatedResponses;
        }
    }
}
