using BlogApi.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.Entities
{
    public class User 
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }


    }
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public UserEntityConfiguration() { }
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                .HasColumnName("userName")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName("email")
                .HasColumnType("nvarchar(60)")
                .IsRequired();
        }
    }

}
