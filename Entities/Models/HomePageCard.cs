using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class HomePageCard
    {
        public int HomePageCardId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string LinkLabel { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public ICollection<HomePageCardCategory> CategoryLinks { get; set; } = new List<HomePageCardCategory>();
    }
}

