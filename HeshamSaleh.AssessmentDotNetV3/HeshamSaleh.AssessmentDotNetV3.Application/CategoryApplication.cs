using AutoMapper;
using Flunt.Notifications;
using HeshamSaleh.AssessmentDotNetV3.Application.Interfaces;
using HeshamSaleh.AssessmentDotNetV3.Application.Results;
using HeshamSaleh.AssessmentDotNetV3.Domain.Entities;
using HeshamSaleh.AssessmentDotNetV3.Domain.Models;
using HeshamSaleh.AssessmentDotNetV3.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace HeshamSaleh.AssessmentDotNetV3.Application
{
    public class CategoryApplication : Notifiable<Notification>, ICategoryApplication
    {
        private readonly IMapper _mapper;

        private readonly ICategoryRepository _categoryRepository;

        public CategoryApplication(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;

            _categoryRepository = categoryRepository;
        }

        public async Task<Result<IEnumerable<CategoryModel>>> GetAsync()
        {
            var categories = await _categoryRepository.SelectAllAsync();

            return Result<IEnumerable<CategoryModel>>.Ok(_mapper.Map<IEnumerable<CategoryModel>>(categories));
        }

        public async Task<Result<CategoryModel>> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.SelectByIdAsync(id);

            if (category != null)
                return Result<CategoryModel>.Ok(_mapper.Map<CategoryModel>(category));

            AddNotification(nameof(category.Id), "No record Found for the given Id");
            return Result<CategoryModel>.Error(Notifications);
        }

        public async Task<Result> PostAsync(CategoryModel category)
        {
            AreValidInformations(category);

            if (!IsValid)
                return Result.Error(Notifications);

            try
            {
                await _categoryRepository.AddAsync(_mapper.Map<Category>(category));

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(Category.Id), ex.InnerException?.Message ?? ex.Message)
                }));
            }
        }

        private void AreValidInformations(CategoryModel category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                AddNotification(nameof(category.Name), $"Field \"{nameof(category.Name)}\" cannot be null or empty.");
        }

        public async Task<Result> PutAsync(Guid id, CategoryModel category)
        {
            AreValidInformations(category);

            if (!IsValid)
                return Result.Error(Notifications);
            try
            {
                var mappedCategory = _mapper.Map<Category>(category);
                mappedCategory.Id = id;
                await _categoryRepository.UpdateAsync(mappedCategory);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(Category.Id), ex.InnerException?.Message ?? ex.Message)
                }));
            }
        }

        public async Task<Result> DeleteAsync(Guid categoryId)
        {
            try
            {
                await _categoryRepository.RemoveAsync(categoryId);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(Category.Id), ex.InnerException?.Message ?? ex.Message)
                }));
            }
        }
    }
}
