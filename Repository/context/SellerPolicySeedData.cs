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
            public class SellerPolicySeedData : IEntityTypeConfiguration<SellerPolicy>
    {
        public void Configure(EntityTypeBuilder<SellerPolicy> builder)
        {
            builder.HasData(
                new SellerPolicy() { SellerPolicyId = 1, SellerPolicyName = "can_sell_lower" },
                new SellerPolicy() { SellerPolicyId = 2, SellerPolicyName = "can_sell_middle" },
                new SellerPolicy() { SellerPolicyId = 3, SellerPolicyName = "can_sell_upper" },
                new SellerPolicy() { SellerPolicyId = 4, SellerPolicyName = "can_sell_everywhere" });


        }
    }
}
