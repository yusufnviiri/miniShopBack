using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class HomeProductCardDto
    {
        public int HomePageCardId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string LinkLabel { get; set; } = string.Empty;
        public bool IsRow { get; set; } = false;
        public int Index { get; set; }
        public ICollection<HomePageProductDto> Products { get; set; } = new List<HomePageProductDto>();

    }
}
