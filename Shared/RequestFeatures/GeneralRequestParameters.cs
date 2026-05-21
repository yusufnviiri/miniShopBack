using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
  public class GeneralRequestParameters : RequestParameters {
        public string? Filter { get; set; }
        public string? OrderBy { get; set; }
    }
}
