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
    public class SellerOfferingSeedData : IEntityTypeConfiguration<SellerOffering>
    {
        public void Configure(EntityTypeBuilder<SellerOffering> builder)
        {
            builder.HasData(
                new SellerOffering() { SellerOfferingId = 1, SellerOfferingName = "Product Seller" },
                new SellerOffering() { SellerOfferingId = 2, SellerOfferingName = "Service Provider" });

        }
    }

}

