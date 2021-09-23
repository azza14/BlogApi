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
using Microsoft.AspNetCore.Authorization;

namespace BlogApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private IGenericRepository<Article> _repoArtical;
        public ArticlesController(IGenericRepository<Article> repo)
        {
            _repoArtical = repo;
        }
        // GET: api/Articles
        [HttpGet]
        public IActionResult GetArticles()
        {
            var list = _repoArtical.GetAll();
            return Ok(list);
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public IActionResult GetArticle(int id)
        {
            var category = _repoArtical.GetById(id);
            return Ok(category);
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutArticle(int id, Article article)
        {
            
            try
            {
                if (id != article.Id)
                {
                    return BadRequest();
                }
                _repoArtical.Update(article);
                _repoArtical.Save();
                return Ok();
            }
            catch 
            {
                 throw;
                
            }

        }

        // POST: api/Articles
        [HttpPost]
        public IActionResult CreateArticle(ArticleDTO model)
        {
            var artical = new Article()
            {
                Id = model.Id,
                Title = model.Title,
                Author= model.Author,
                Content= model.Content,
                CreatedAt= DateTime.Now,
                IsPublished= true,
                ModifidedAt= model.ModifidedAt,
                CategoryId=model.CategoryId
            };
            try
            {
                _repoArtical.Insert(artical);
                _repoArtical.Save();
                return Ok(new { message = "Create Artical successfully" });

            }
            catch
            {
                throw;
            }
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public IActionResult DeleteArticle(int id)
        {
            var artical = _repoArtical.GetById(id);
            if (artical == null)
            {
                return NotFound();
            }
            _repoArtical.Delete(id);
            _repoArtical.Save();
            return Ok(new { message = "Deleted Artical successfully" });
        }

    }
}
