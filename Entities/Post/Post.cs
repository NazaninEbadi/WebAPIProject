using Entities.Common;
using Entities.User;
using Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Post
{
    //public class Post:BaseEntity<Guid>
     public class Post : BaseEntity
     {
        public string Title { get; set; }

        public string Description { get; set; }
        
        public int CategoryId { get; set; }
       
        public Category Category { get; set; }

        public int CreatedById { get; set; }
       
        //[ForeignKey(nameof(CreatedbyId))]
        public User.User CreatedBy { get; set; }
       

    }

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    
    {

        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(200);
            builder.HasOne(p => p.Category).WithMany(c => c.Posts).HasForeignKey(p => p.CategoryId);
            builder.HasOne(p => p.CreatedBy).WithMany(q => q.Posts).HasForeignKey(c => c.CreatedById);
              
        }
    }
}

