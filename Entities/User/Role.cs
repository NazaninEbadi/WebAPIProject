using Entities.Common;
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
    public class Role:BaseEntity
    {
    
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }

    }



    public class RoleConfiguration:IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(100);
            builder.HasOne(p => p.CreatedBy).WithMany( q => q.Roles).HasForeignKey(p => p.CreatedById);
        }
    }
}
