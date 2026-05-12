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
        public bool IsRow { get; set; } = false;
        public int Index { get; set; }

        public string? SellerSlugName { get; set; } = string.Empty;

        public Guid? UserGroupId { get; set; } =Guid.Empty;
        public bool IsGroupCard { get; set; } = false;
        public ICollection<HomePageCardCategory> CategoryLinks { get; set; } = new List<HomePageCardCategory>();
    }
}

