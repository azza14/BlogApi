using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifidedAt { get; set; }
        public bool IsPublished{ get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Comment> Comments { get; set; }
    }

    public class ArticaleEntityConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(c => c.Id)
               .HasColumnType("int")
               .HasColumnName("Id")
               .IsRequired()
               .UseIdentityColumn()
               .ValueGeneratedOnAdd();

            builder.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)")
                .IsRequired();


            builder.Property(d => d.Content)
                .HasMaxLength(100)
                .HasColumnType("nvarchar(250)")
                .IsRequired() ;

            builder.Property(t => t.CreatedAt)
                .HasMaxLength(10)
                .HasColumnType("date");

            builder.Property(t => t.ModifidedAt)
               .HasMaxLength(10)
               .HasColumnType("date");

            builder.Property(t => t.IsPublished);
               
            //relations
            builder.HasOne<Category>(c => c.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(f => f.CategoryId);




        }


    }
}
