using Common.Enums;
using Entities.Common;
using Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.User
{
    public class User:BaseEntity
    {
        public User()
        {
            IsActive = true;
        }

      // [Required]
       //[StringLength(100)]
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public bool IsActive { get; set; }

       // [Required,StringLength(100)]
        public string PasswordHash { get; set; }
        public int Age { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime ModifyOn { get; set; }
        public ICollection<Post.Post> Posts { get; set; }  
        public ICollection<Category> Categories { get; set; }
        public ICollection<Role> Roles { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.PasswordHash).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Gender).IsRequired();
            builder.Property(p => p.Age).IsRequired();
            builder.Property(p => p.LastLoginDate).IsRequired();
            builder.HasMany(p => p.Posts).WithOne(q => q.CreatedBy).HasForeignKey(c => c.CreatedById);
            builder.HasMany(p => p.Categories).WithOne(q => q.CreatedBy).HasForeignKey(c => c.CreatedById);
           
        }
    }
}
