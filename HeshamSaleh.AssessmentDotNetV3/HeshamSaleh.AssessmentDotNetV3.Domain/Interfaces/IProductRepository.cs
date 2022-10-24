using HeshamSaleh.AssessmentDotNetV3.Domain.Core.Interfaces;
using HeshamSaleh.AssessmentDotNetV3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeshamSaleh.AssessmentDotNetV3.Domain.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> SelectByCategoryIdAsync(Guid categoryId);
    }
}
