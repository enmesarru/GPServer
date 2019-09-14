using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GServer.Api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IReadOnlyList<Category>> Get() => await categoryRepository.ListAllAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if(category == null) 
            {
                return NotFound();
            }
            return new OkObjectResult(category);
        }
    }
}