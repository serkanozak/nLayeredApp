using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Dtos.Requests.CategoryRequests;
using Business.Dtos.Responses.CategoryResponses;
using Core.DataAccess.Paging;
using Entities.Concretes;

namespace Business.Profiles
{
    public class CategoryMappingProfiles : Profile
    {
        public CategoryMappingProfiles()
        {
            CreateMap<CreateCategoryRequest, Category>().ReverseMap();
            CreateMap<Category, CreatedCategoryResponse>().ReverseMap();

            CreateMap<DeleteCategoryRequest, Category>().ReverseMap();
            CreateMap<Category, DeletedCategoryResponse>().ReverseMap();

            CreateMap<UpdateCategoryRequest, Category>().ReverseMap();
            CreateMap<Category, UpdatedCategoryResponse>().ReverseMap();

            CreateMap<GetListedCategoryResponse, Category>().ReverseMap();
            CreateMap<Paginate<GetListedCategoryResponse>, Paginate<Category>>().ReverseMap();
        }
    }
}
