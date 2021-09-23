using System;

namespace BlogApi.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Title { get;  set; }
        public string Author { get;  set; }
        public string Content { get;  set; }
        public DateTime ModifidedAt { get;  set; }
        public int CategoryId { get;  set; }
    }
}