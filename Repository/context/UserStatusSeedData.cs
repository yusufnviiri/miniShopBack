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
          public class UserStatusSeedData : IEntityTypeConfiguration<UserStatus>
    {
        public void Configure(EntityTypeBuilder<UserStatus> builder)
        {
            builder.HasData(
                new UserStatus() { UserStatusId = 1, StatusName = "Active" },
                new UserStatus() { UserStatusId = 2, StatusName = "Suspended" },
                new UserStatus() { UserStatusId = 3, StatusName = "Banned" });


        }

    }
}
