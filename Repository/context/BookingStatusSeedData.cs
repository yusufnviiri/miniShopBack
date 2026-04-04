using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.context
{
    public class BookingStatusSeedData : IEntityTypeConfiguration<BookingStatus>
    {
        public void Configure(EntityTypeBuilder<BookingStatus> builder)
        {
            builder.HasData(
                new BookingStatus() { Status = "Pending", BookingStatusId = 1 },
                new BookingStatus() { Status = "Accepted", BookingStatusId = 2 },
                new BookingStatus() { Status = "Rejected", BookingStatusId = 3 },
                new BookingStatus() { Status = "InProgress", BookingStatusId = 4 },
                new BookingStatus() { Status = "Completed", BookingStatusId = 5 },
                new BookingStatus() { Status = "Cancelled", BookingStatusId = 6 },
                new BookingStatus() { Status = "Disputed", BookingStatusId = 7 });
        }
    }

}
