using HeshamSaleh.AssessmentDotNetV3.Application.Interfaces;
using HeshamSaleh.AssessmentDotNetV3.Domain.Models;
using HeshamSaleh.AssessmentDotNetV3.Util;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HeshamSaleh.AssessmentDotNetV3.Api.Controllers
{
    [ApiController]
    [Route(Constants.Routs.Category)]
    [EnableCors("AllowOrigin")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryApplication _categoryApplication;

        public CategoryController(ICategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryApplication.GetAsync();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetCategoryByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _categoryApplication.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryModel category)
        {
            var result = await _categoryApplication.PostAsync(category);

            if (!result.Success)
                return BadRequest(result);

            return CreatedAtRoute("GetCategoryByIdAsync",
                new { id = result.Data.Id },
                result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CategoryModel category)
        {
            var result = await _categoryApplication.PutAsync(id, category);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _categoryApplication.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
