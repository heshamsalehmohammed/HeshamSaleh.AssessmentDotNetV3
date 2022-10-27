using AutoMapper;
using Flunt.Notifications;
using HeshamSaleh.AssessmentDotNetV3.Application.Interfaces;
using HeshamSaleh.AssessmentDotNetV3.Application.Results;
using HeshamSaleh.AssessmentDotNetV3.Domain.Entities;
using HeshamSaleh.AssessmentDotNetV3.Domain.Models;
using HeshamSaleh.AssessmentDotNetV3.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace HeshamSaleh.AssessmentDotNetV3.Application
{
    public class ProductApplication : Notifiable<Notification>, IProductApplication
    {
        private readonly IMapper _mapper;

        private readonly IProductRepository _productRepository;

        public ProductApplication(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;

            _productRepository = productRepository;
        }

        public async Task<Result<IEnumerable<ProductModel>>> GetAsync()
        {
            var products = await _productRepository.SelectAllAsync();

            return Result<IEnumerable<ProductModel>>.Ok(_mapper.Map<IEnumerable<ProductModel>>(products));
        }

        public async Task<Result<IEnumerable<ProductModel>>> GetByCategoryIdAsync(Guid categoryId)
        {
            var products = await _productRepository.SelectByCategoryIdAsync(categoryId);

            return Result<IEnumerable<ProductModel>>.Ok(_mapper.Map<IEnumerable<ProductModel>>(products));
        }

        public async Task<Result<ProductModel>> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.SelectByIdAsync(id);

            if (product != null)
                return Result<ProductModel>.Ok(_mapper.Map<ProductModel>(product));

            AddNotification(nameof(product.Id), "No record Found for the given Id");
            return Result<ProductModel>.Error(Notifications);
        }

        public async Task<Result> PostAsync(ProductModel product)
        {
            AreValidInformations(product);

            if (!IsValid)
                return Result.Error(Notifications);

            try
            {
                await _productRepository.AddAsync(_mapper.Map<Product>(product));

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(Product.Id), ex.InnerException.Message ?? ex.Message)
                }));
            }
        }

        private void AreValidInformations(ProductModel product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                AddNotification(nameof(product.Name), $"Field \"{nameof(product.Name)}\" cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(product.Price))
                AddNotification(nameof(product.Price), $"Field \"{nameof(product.Price)}\" cannot be null or empty.");

            if (product.Quantity == default)
                AddNotification(nameof(product.Quantity), $"Field \"{nameof(product.Quantity)}\"  must be greater than or equal to 0.");

            if (product.CategoryId == Guid.Empty)
                AddNotification(nameof(product.CategoryId), $"Field \"{nameof(product.CategoryId)}\"  must be greater than or equal to 0.");
        }

        public async Task<Result> PutAsync(Guid id, ProductModel product)
        {
            AreValidInformations(product);

            if (!IsValid)
                return Result.Error(Notifications);

            try
            {
                var mappedProduct = _mapper.Map<Product>(product);
                mappedProduct.Id = id;
                await _productRepository.UpdateAsync(mappedProduct);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(Product.Id), ex.InnerException.Message ?? ex.Message)
                }));
            }
        }

        public async Task<Result> PatchAsync(Guid id, JsonPatchDocument<ProductModel> patchEntity)
        {
            try
            {
                var productFromRepo = await _productRepository.SelectByIdAsync(id);
                if (productFromRepo == null) throw new ArgumentNullException(nameof(productFromRepo));

                var productModelFromRepo = _mapper.Map<ProductModel>(productFromRepo);

                patchEntity.ApplyTo(productModelFromRepo);

                AreValidInformations(productModelFromRepo);

                if (!IsValid)
                    return Result.Error(Notifications);

                await _productRepository.UpdateAsync(_mapper.Map<Product>(productModelFromRepo));

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(Product.Id), ex.InnerException?.Message ?? ex.Message)
                }));
            }
        }

        public async Task<Result> DeleteAsync(Guid productId)
        {
            try
            {
                await _productRepository.RemoveAsync(productId);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(Product.Id), ex.InnerException.Message ?? ex.Message)
                }));
            }
        }
    }
}
