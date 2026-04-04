using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class RefreshTokenDto
    {


             public string? AccessToken { get; set; }
            public string? RefreshTokenData { get; set; }
        public MiniUserDataDto? UserData { get; set; }

   
    }
}
