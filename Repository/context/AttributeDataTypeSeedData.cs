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
         public class AttributeDataTypeSeedData : IEntityTypeConfiguration<AttributeDataType>
    {
        public void Configure(EntityTypeBuilder<AttributeDataType> builder)
        {
            builder.HasData(
                new AttributeDataType() { AttributeDataTypeId = 1, DataTypeName = "String" },
                new AttributeDataType() { AttributeDataTypeId = 2, DataTypeName = "Int" },
                new AttributeDataType() { AttributeDataTypeId = 3, DataTypeName = "Decimal" },
                new AttributeDataType() { AttributeDataTypeId = 4, DataTypeName = "Bool" }, new AttributeDataType() { AttributeDataTypeId = 5, DataTypeName = "Date" });


        }
    }
}
