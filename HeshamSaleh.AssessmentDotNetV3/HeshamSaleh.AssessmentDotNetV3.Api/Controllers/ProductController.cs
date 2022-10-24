using HeshamSaleh.AssessmentDotNetV3.Application.Interfaces;
using HeshamSaleh.AssessmentDotNetV3.Application.Results;
using HeshamSaleh.AssessmentDotNetV3.Domain.Entities;
using HeshamSaleh.AssessmentDotNetV3.Domain.Models;
using HeshamSaleh.AssessmentDotNetV3.Util;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeshamSaleh.AssessmentDotNetV3.Api.Controllers
{
    [ApiController]
    [Route(Constants.Routs.Product)]
    [EnableCors("AllowOrigin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;

        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid categoryId)
        {
            Result<IEnumerable<ProductModel>> result;
            if (categoryId == Guid.Empty)
                result = await _productApplication.GetAsync();
            else
                result = await _productApplication.GetByCategoryIdAsync(categoryId);

            if (result == null)
                return NotFound(Constants.ReturnMessages.NotFound);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _productApplication.GetByIdAsync(id);

            if (result == null)
                return NotFound(Constants.ReturnMessages.NotFound);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductModel product)
        {
            var result = await _productApplication.PostAsync(product);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProductModel product)
        {
            var result = await _productApplication.PutAsync(id, product);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<ProductModel> patchEntity)
        {
            var result = await _productApplication.PatchAsync(id, patchEntity);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productApplication.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }
    }
}
