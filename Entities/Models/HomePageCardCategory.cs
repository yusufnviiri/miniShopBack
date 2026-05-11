using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class HomePageCardCategory
    {
        public int HomePageCardCategoryId { get; set; }
        public int HomePageCardId { get; set; }
        public int CategoryId { get; set; }
        public Guid UserGroupId { get; set; } = Guid.NewGuid();     
        public bool IsGroupCard { get; set; }=false;
        public HomePageCard? HomePageCard { get; set; }
    }
}
