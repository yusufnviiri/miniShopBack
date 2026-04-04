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
            public class BuyerTierSeedData : IEntityTypeConfiguration<BuyerTier>
    {
        public void Configure(EntityTypeBuilder<BuyerTier> builder)
        {
            builder.HasData(
                new BuyerTier() { BuyerTierId = 1, Description = "REGULAR" },
                new BuyerTier() { BuyerTierId = 2, Description = "VIP" },
                new BuyerTier() { BuyerTierId = 3, Description = "INSTITUTIONAL" });


        }
    }
}
