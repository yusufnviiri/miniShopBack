using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
  
        public class TradeImage
        {
        public Guid TradeImageId { get; set; }

            public Trade? Trade { get; set; }
            public Guid? TradeId { get; set; }
            public bool IsPrimary { get; set; }
            // File metadata
            public string Folder { get; set; } = null!;   // e.g. images/products/{guid}
            public string FileName { get; set; } = null!; // base name, not size-specific
            public bool IsProcessed { get; set; }
            public DateTime CreatedAt { get; set; }


        }
    }

