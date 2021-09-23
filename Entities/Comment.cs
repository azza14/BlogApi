using BlogApi.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifidedAt { get; set; }
        public ApprovedStatusEnum ApprovedStatus  { get; set; }
        public string Reason  { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }


    }
    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Text)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            //relation
            builder.HasOne<Article>(c => c.Article)
               .WithMany(c => c.Comments)
               .HasForeignKey(f => f.ArticleId);
        }
    }
}
