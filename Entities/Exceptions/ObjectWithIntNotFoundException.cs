using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ObjectWithIntNotFoundException : NotFoundException
    {
        public ObjectWithIntNotFoundException(int objectId)
        : base($"The Object with id: {objectId} doesn't exist in the database.")
        {
        }
    }
    
}
