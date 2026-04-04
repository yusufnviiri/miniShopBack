using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ProductImageDto
    {
        public Guid ProductImageId { get; set; }
        public Product? Product { get; set; }
    

        public Guid? ProductId { get; set; }
        public bool IsPrimary { get; set; }

        // File metadata
        public string Folder { get; set; } = null!;   // e.g. images/products/{guid}
        public string FileName { get; set; } = null!; // base name, not size-specific

        public bool IsProcessed { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
