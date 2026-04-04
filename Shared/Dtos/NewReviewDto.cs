using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class NewReviewDto
    {
        public Guid ProductId { get; set; }
        public string ApplicationUserId { get; set; }= default!;
        public int Rating { get; set; } 
        public string Comment { get; set; } = default!;
        public Guid ReviewId { get; set; }


    }
}
