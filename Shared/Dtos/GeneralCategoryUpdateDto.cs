using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class GeneralCategoryUpdateDto
    {
        public int GeneralCategoryId { get; set; }
        public string GeneralCategoryName { get; set; } = string.Empty;
        public ICollection<int> CategoryIds { get; set; } = [];
    }
}
