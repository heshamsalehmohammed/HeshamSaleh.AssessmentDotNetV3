using HeshamSaleh.AssessmentDotNetV3.Domain.Entities;
using HeshamSaleh.AssessmentDotNetV3.Domain.Repositories.Interfaces;
using HeshamSaleh.AssessmentDotNetV3.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HeshamSaleh.AssessmentDotNetV3.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public DBContext _context { get; }

        public ProductRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Product product)
        {
            _context.Add(product);

            return await _context.SaveChangesAsync();
        }

        public IQueryable<Product> GetByCondition(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> SelectAllAsync()
        {
            IEnumerable<Product> wfList = null;

            await Task.Run(() =>
            {
                wfList = _context.Product;
            });

            return wfList;
        }

        public async Task<IEnumerable<Product>> SelectByCategoryIdAsync(Guid categoryId)
        {
            IEnumerable<Product> wfList = null;

            await Task.Run(() =>
            {
                wfList = _context.Product.Where(p => p.CategoryId == categoryId);
            });

            return wfList;
        }

        public async Task<Product> SelectByIdAsync(Guid id)
        {
            return await _context.Product.FindAsync(id);
        }

        public async Task<int> UpdateAsync(Product newProduct)
        {
            var oldProduct = await SelectByIdAsync(newProduct.Id);

            _context.Entry(oldProduct).CurrentValues.SetValues(newProduct);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(Guid id)
        {
            var product = await SelectByIdAsync(id);

            if (product != null)
            {
                _context.Remove(product);

                return await _context.SaveChangesAsync();
            }
            else
                return 0;
        }
    }
}
