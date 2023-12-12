using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Dtos.Requests.ProductRequests;
using Business.Dtos.Responses.ProductResponses;
using Core.DataAccess.Paging;
using Entities.Concretes;

namespace Business.Mapping
{
    public class ProductMappingProfiles : Profile
    {
        public ProductMappingProfiles()
        {
            CreateMap<CreateProductRequest, Product>().ReverseMap();
            CreateMap<Product, CreatedProductResponse>().ReverseMap();

            CreateMap<Product, GetListedProductResponse>().ReverseMap();
            CreateMap<Paginate<Product>, Paginate<GetListedProductResponse>>().ReverseMap();

            CreateMap<Product, GetProductResponse>().ReverseMap();

            CreateMap<UpdateProductRequest, Product>().ReverseMap();
            CreateMap<Product, UpdatedProductResponse>().ReverseMap();

            CreateMap<DeleteProductRequest, Product>().ReverseMap();
            CreateMap<Product, DeletedProductResponse>().ReverseMap();

            CreateMap<Product, GetListedProductResponse>()
                .ForMember(destinationMember: p => p.CategoryId,
                memberOptions: opt => opt.MapFrom(p => p.CategoryId)).ReverseMap();

            CreateMap<Product, GetListedProductResponse>()
                .ForMember(destinationMember: p => p.CategoryName,
                memberOptions: opt => opt.MapFrom(p => p.Category.CategoryName)).ReverseMap();
        }
    }
}
