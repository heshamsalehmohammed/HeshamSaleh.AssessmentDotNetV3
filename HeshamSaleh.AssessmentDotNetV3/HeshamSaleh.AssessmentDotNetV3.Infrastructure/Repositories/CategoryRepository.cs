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
    public class CategoryRepository : ICategoryRepository
    {
        public DBContext _context { get; }

        public CategoryRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Category category)
        {
            _context.Add(category);

            return await _context.SaveChangesAsync();
        }

        public IQueryable<Category> GetByCondition(Expression<Func<Category, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> SelectAllAsync()
        {
            IEnumerable<Category> wfList = null;

            await Task.Run(() =>
            {
                wfList = _context.Category;
            });

            return wfList;
        }

        public async Task<Category> SelectByIdAsync(Guid id)
        {
            return await _context.Category.FindAsync(id);
        }

        public async Task<int> UpdateAsync(Category newCategory)
        {
            var oldCategory = await SelectByIdAsync(newCategory.Id);

            _context.Entry(oldCategory).CurrentValues.SetValues(newCategory);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(Guid id)
        {
            var category = await SelectByIdAsync(id);

            if (category != null)
            {
                _context.Remove(category);

                return await _context.SaveChangesAsync();
            }
            else
                return 0;
        }
    }
}
