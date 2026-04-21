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
   public class HomePageCardRoleSeedData : IEntityTypeConfiguration<HomeCardRole>
    {
        public void Configure(EntityTypeBuilder<HomeCardRole> builder)
        {
            builder.HasData(
                new HomeCardRole() { HomeCardRoleName = "HomePageProducts", HomeCardRoleId = 1 },
                new HomeCardRole() { HomeCardRoleName = "GroupProducts", HomeCardRoleId = 2 }
               


      );
        }
    }
}
