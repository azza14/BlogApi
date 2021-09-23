using BlogApi.Entities;
using BlogApi.Repositories;

namespace BlogApi.Auth
{
    public class CategorytRepository : GenericRepository<Category>
    {
        public CategorytRepository(GenericRepository<Category> repository):base( repository)
        {
            Repository = repository;
        }
        public GenericRepository<Category> Repository { get; }
    }
    
}
