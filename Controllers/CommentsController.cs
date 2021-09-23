using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogApi.Entities;
using BlogApi.Repositories;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private IGenericRepository<Comment> _repoComment;
        public CommentsController(IGenericRepository<Comment> repo)
        {
            _repoComment = repo;
        }
        // GET: api/Comments
        [HttpGet]
        public IActionResult GetComments()
        {
            var list = _repoComment.GetAll();
            return Ok(list);
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var comment = _repoComment.GetById(id);
            return Ok(comment);
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutComment(int id, Comment comment)
        {
            try
            {
                if (id != comment.Id)
                {
                    return BadRequest();
                }
                _repoComment.Update(comment);
                _repoComment.Save();
                return Ok();
            }
            catch
            {
                throw;

            }
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostComment(Comment comment)
        {
            try
            {
                _repoComment.Insert(comment);
                _repoComment.Save();
                return Ok(new { message = "Create Comment successfully" });

            }
            catch
            {
                throw;
            }
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var comment = _repoComment.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }
            _repoComment.Delete(id);
            _repoComment.Save();
            return Ok(new { message = "Deleted comment successfully" });
        }

    }
}
