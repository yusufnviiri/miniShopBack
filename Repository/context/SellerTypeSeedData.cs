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
    public class SellerTypeSeedData : IEntityTypeConfiguration<SellerType>
    {
        public void Configure(EntityTypeBuilder<SellerType> builder)
        {
            builder.HasData(
                new SellerType() { SellerTypeId = 1, SellerTypeName = "Individual" },
                new SellerType() { SellerTypeId = 2, SellerTypeName = "Group" });

        }
    }
}
