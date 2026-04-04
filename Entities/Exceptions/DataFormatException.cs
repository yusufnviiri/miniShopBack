using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
     public sealed class DataFormatException : NotFoundException
    {
        public DataFormatException(string message)
        : base(message)
        {
        }
    }
   
}
