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
    public class BuyerTypeSeedData : IEntityTypeConfiguration<BuyerType>
    {
        public void Configure(EntityTypeBuilder<BuyerType> builder)
        {
            builder.HasData(
                new BuyerType() { BuyerTypeId = 1, BuyerTypeName = "Individual"},
                new BuyerType() { BuyerTypeId = 2, BuyerTypeName = "Group" });

        }
    }
}
