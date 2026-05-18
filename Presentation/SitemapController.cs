using Contracts.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Presentation
{

    [ApiController]
    [Route("")]
    public class SitemapController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IConfiguration _config;

        public SitemapController(IServiceManager service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        [HttpGet("sitemap.xml")]
        [ResponseCache(Duration = 3600)]
        public async Task Sitemap()
        {
            var siteUrl = _config["SiteUrl"]?.TrimEnd('/') ?? "https://buypamoja.com";

            var productSlugs = await _service.ProductService.GetAllProductSlugsAsync();
            var sellerSlugs = await _service.SellerProfileService.GetAllSellerSlugsAsync();
            var tradeSlugs = await _service.TradeService.GetAllTradesSlugsAsync();
            //var categories = await _service.CategoryService.GetAllCategorySlugsAsync();

            Response.ContentType = "application/xml; charset=utf-8";

            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = new UTF8Encoding(false), // no BOM
                Async = true,
            };

            await using var writer = XmlWriter.Create(Response.Body, settings);

            await writer.WriteStartDocumentAsync();
            writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

            WriteUrl(writer, $"{siteUrl}/", DateTime.UtcNow, "daily", "1.0");

            //foreach (var c in categories)
            //    WriteUrl(writer, $"{siteUrl}/category/{SlugifyForUrl(c.Slug)}",
            //        c.UpdatedAt, "weekly", "0.7");

            foreach (var s in sellerSlugs)
                WriteUrl(writer, $"{siteUrl}/sellers/{SlugifyForUrl(s.Slug)}",
                    s.UpdatedAt, "weekly", "0.6");

            foreach (var p in productSlugs)
            {
                if (LooksLikePlaceholderSlug(p.Slug)) continue;          // see Problem 2
                WriteUrl(writer, $"{siteUrl}/products/{SlugifyForUrl(p.Slug)}",
                    p.UpdatedAt, "weekly", "0.8");
            }

            foreach (var t in tradeSlugs)
            {
                if (LooksLikePlaceholderSlug(t.Slug)) continue;
                WriteUrl(writer, $"{siteUrl}/trades/{SlugifyForUrl(t.Slug)}",
                    t.UpdatedAt, "weekly", "0.8");
            }

            await writer.WriteEndElementAsync();
            await writer.WriteEndDocumentAsync();
            await writer.FlushAsync();
        }

        // URL-encode and convert spaces to "-"
        private static string SlugifyForUrl(string slug)
        {
            if (string.IsNullOrEmpty(slug)) return "";
            return Uri.EscapeDataString(slug.Trim().Replace(' ', '-'));
        }

        // Heuristic for obvious test data
        private static bool LooksLikePlaceholderSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return true;
            var s = slug.ToLowerInvariant();

            // Repeated single-character runs (rrrrrrr, vvvvvvvvvvvvv, xxxxxx, gggggg)
            var stripped = System.Text.RegularExpressions.Regex.Replace(s, @"-\d+$", "");
            if (stripped.Length >= 3 &&
                stripped.All(ch => ch == stripped[0]))
                return true;

            // Known test garbage
            string[] testTokens = { "test", "yujgj", "ddddd", "fffff" };
            if (testTokens.Any(t => stripped.Contains(t))) return true;

            return false;
        }













        private static void WriteUrl(
            XmlWriter w,
            string loc,
            DateTime? lastmod,
            string changefreq,
            string priority)
        {
            w.WriteStartElement("url");
            w.WriteElementString("loc", loc);
            if (lastmod.HasValue)
                w.WriteElementString("lastmod", lastmod.Value.ToString("yyyy-MM-dd"));
            w.WriteElementString("changefreq", changefreq);
            w.WriteElementString("priority", priority);
            w.WriteEndElement();
        }
    }
}
