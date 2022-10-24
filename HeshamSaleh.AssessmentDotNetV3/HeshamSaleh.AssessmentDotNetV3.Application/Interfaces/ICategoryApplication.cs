using HeshamSaleh.AssessmentDotNetV3.Application.Results;
using HeshamSaleh.AssessmentDotNetV3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeshamSaleh.AssessmentDotNetV3.Application.Interfaces
{
    public interface ICategoryApplication
    {
        public Task<Result<IEnumerable<CategoryModel>>> GetAsync();

        public Task<Result<CategoryModel>> GetByIdAsync(Guid id);

        public Task<Result> PostAsync(CategoryModel category);

        public Task<Result> PutAsync(Guid id, CategoryModel category);

        public Task<Result> DeleteAsync(Guid categoryId);
    }
}
