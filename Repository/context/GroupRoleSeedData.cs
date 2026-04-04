using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.context
{
       public class GroupRoleSeedData : IEntityTypeConfiguration<GroupRole>
    {
        public void Configure(EntityTypeBuilder<GroupRole> builder)
        {
            builder.HasData(
                new GroupRole() { Description = "Member", GroupRoleId = 1 },
                new GroupRole() { Description = "Treasurer", GroupRoleId = 2 },
                new GroupRole() { Description = "Secretary", GroupRoleId = 3 },
                new GroupRole() { Description = "Chairperson", GroupRoleId = 4 },
                new GroupRole() { Description = "Admin", GroupRoleId = 5 },
                new GroupRole() { Description = "Auditor", GroupRoleId = 6 }


      );
        }
    }

}
