using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
   [ApiController]
    [Route("api/trade/images")]
    public class TradeImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public TradeImageController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("{tradeId}/{fileName}/{size}")]

        public IActionResult Get(Guid tradeId, string fileName, string size)
        {
            var path = Path.Combine(
                _env.WebRootPath,        // ✅ ROOTED PATH
                "uploads",
                "trades",
                "images",
                tradeId.ToString(),
                $"{fileName}_{size}.webp"


            );

            if (!System.IO.File.Exists(path))
                return GetFallback(size);

            Response.Headers.CacheControl = "public, max-age=31536000, immutable";
            return PhysicalFile(path, "image/webp", enableRangeProcessing: true);
        }



        private IActionResult GetFallback(string size)
        {

            var fallbackPath = Path.Combine(
                _env.WebRootPath,
                   "uploads",
                  "fallback",
                  $"{size}.PNG"
              );

            if (!System.IO.File.Exists(fallbackPath))
                return NotFound();

            Response.Headers.CacheControl = "public, max-age=60"; // short cache
            return PhysicalFile(fallbackPath, "image/webp");
        }



        


    }
}

