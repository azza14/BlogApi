using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogApi.Entities;
using BlogApi.Repositories;
using BlogApi.DTO;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private IGenericRepository<Category> _repoCategory;
        public CategoriesController(IGenericRepository<Category> repoCategory)
        {
            _repoCategory = repoCategory;
        }

        // GET: api/Categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            var list= _repoCategory.GetAll();
            return Ok(list);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _repoCategory.GetById(id);
            return Ok(category);
        }
        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public IActionResult PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            _repoCategory.Update(category);
            _repoCategory.Save();
            return Ok();
        }

        // POST: api/Categories
        [HttpPost]
        public IActionResult  CreateCategory([FromBody] CategorytDTO model)
        {
            var category = new Category()
            {
                Id = model.Id,
                Name = model.Name
            };
            try
            {
                _repoCategory.Insert(category);
                _repoCategory.Save();
                return Ok(new { message = "Create Category successfully" });

            }
            catch
            {
                throw;
            }
           

        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category =  _repoCategory.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            _repoCategory.Delete(id);
            _repoCategory.Save();
            return Ok(new { message = "Deleted Category successfully" });
        }
    }
}
