using HeshamSaleh.AssessmentDotNetV3.Application.Results;
using HeshamSaleh.AssessmentDotNetV3.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeshamSaleh.AssessmentDotNetV3.Application.Interfaces
{
    public interface IProductApplication
    {
        public Task<Result<IEnumerable<ProductModel>>> GetAsync();

        public Task<Result<ProductModel>> GetByIdAsync(Guid id);

        public Task<Result<IEnumerable<ProductModel>>> GetByCategoryIdAsync(Guid categoryId);

        public Task<Result> PostAsync(ProductModel product);

        public Task<Result> PutAsync(Guid id, ProductModel product);

        public Task<Result> PatchAsync(Guid id, JsonPatchDocument<ProductModel> patchEntity);

        public Task<Result> DeleteAsync(Guid productId);
    }
}
