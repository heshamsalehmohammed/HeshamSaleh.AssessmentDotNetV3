using AutoMapper;
using HeshamSaleh.AssessmentDotNetV3.Domain.Entities;
using HeshamSaleh.AssessmentDotNetV3.Domain.Models;

namespace HeshamSaleh.AssessmentDotNetV3.Application.Mapping
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();
        }
    }
}
