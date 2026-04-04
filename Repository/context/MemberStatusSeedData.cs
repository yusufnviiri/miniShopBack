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
         public class MemberStatusSeedData : IEntityTypeConfiguration<MemberStatus>
    {
        public void Configure(EntityTypeBuilder<MemberStatus> builder)
        {
            builder.HasData(
                new MemberStatus() { Description = "Active", MemberStatusId = 1 },
                new MemberStatus() { Description = "Suspended", MemberStatusId = 2 },
                new MemberStatus() { Description = "Removed", MemberStatusId = 3 });
        }
    
    }
}
