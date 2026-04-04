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
        public class SellerTierSeedData : IEntityTypeConfiguration<SellerTier>
    {
        public void Configure(EntityTypeBuilder<SellerTier> builder)
        {
            builder.HasData(
                new SellerTier() { SellerTierId = 1, SellerTierDescription = "BASIC" },
                new SellerTier() { SellerTierId = 2, SellerTierDescription = "TRUSTED" },
                new SellerTier() { SellerTierId = 3, SellerTierDescription = "VERIFIED" },
                new SellerTier() { SellerTierId = 4, SellerTierDescription = "RESTRICTED" });


        }
    }
}
