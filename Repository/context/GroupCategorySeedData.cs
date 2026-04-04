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
            public class GroupCategorySeedData : IEntityTypeConfiguration<GroupCategory>
    {
        public void Configure(EntityTypeBuilder<GroupCategory> builder)
        {
            builder.HasData(
                new GroupCategory() { GroupCategoryId = 1, Description = "ECONOMY" },
                new GroupCategory() { GroupCategoryId = 2, Description = "MIDDLE" },
                new GroupCategory() { GroupCategoryId = 3, Description = "PREMIUM" });


        }
    }
}
