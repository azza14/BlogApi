using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Entities
{
    public class Category
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public int Id { get; set; }
        public string Name { get; set; }

       public List<Article> Articles { get; set; }
    }
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            
            builder.Property(c => c.Id)
                .HasColumnType("int")
                .HasColumnName("Id")
                .IsRequired()
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(50)")
                .IsRequired();
        }
    }
}
