using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
     public sealed class ObjectBadRequestExeption : BadRequestException
    {
        public ObjectBadRequestExeption(string message)
        : base(message)
        {
        }
    }

}

