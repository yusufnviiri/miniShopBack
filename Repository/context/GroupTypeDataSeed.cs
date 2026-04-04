using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.context
{
    public class GroupTypeDataSeed : IEntityTypeConfiguration<GroupType>
    {
        public void Configure(EntityTypeBuilder<GroupType> builder)
        {
            builder.HasData(
                new GroupType() { Description = "Saving Cooperative", GroupTypeId = 1 },
                new GroupType() { Description = "Company", GroupTypeId =2},
                new GroupType() { Description = "Church", GroupTypeId =3},
                new GroupType() { Description = "IslamicInstitution", GroupTypeId =4},
                new GroupType() { Description = "School", GroupTypeId =5},
                new GroupType() { Description = "NGO", GroupTypeId =6}


      );
        }
    }
}
