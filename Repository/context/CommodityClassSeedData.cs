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
    public class CommodityClassSeedData : IEntityTypeConfiguration<CommodityClass>
    {
        public void Configure(EntityTypeBuilder<CommodityClass> builder)
        {
            builder.HasData(
                new CommodityClass() { CommodityClassId = 3, CommodityClassName = "Upper Class" },
                new CommodityClass() { CommodityClassId = 2, CommodityClassName = "Middle Class" },
                new CommodityClass() { CommodityClassId = 1, CommodityClassName = "Lower Class" });


        }
    }
}
