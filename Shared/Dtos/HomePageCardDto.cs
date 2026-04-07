using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class HomePageCardDto
    {
        public int HomePageCardId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string LinkLabel { get; set; } = string.Empty;

        public ICollection<MiniCategoryDto> Categories  { get; set; } = [];
    }
}
