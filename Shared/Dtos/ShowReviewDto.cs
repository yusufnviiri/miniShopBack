using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ShowReviewDto
    {

        public Guid ReviewId { get; set; }
        public Guid UserProfileId { get; set; } = default!;
        public string? ReviewerName { get; set; }
        public int Rating { get; set; } // 1–5
        public string Comment { get; set; } = default!;
        public DateTime CreatedAt { get; set; } 
    }
}
