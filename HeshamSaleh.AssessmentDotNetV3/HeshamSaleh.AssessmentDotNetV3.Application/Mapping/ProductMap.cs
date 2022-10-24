using AutoMapper;
using HeshamSaleh.AssessmentDotNetV3.Domain.Entities;
using HeshamSaleh.AssessmentDotNetV3.Domain.Models;

namespace HeshamSaleh.AssessmentDotNetV3.Application.Mapping
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
        }
    }
}
