using Contracts.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Hubs
{
    public sealed class SocialScraperMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceManager _service;
        private readonly IConfiguration _config;

        // User-agent fragments belonging to social preview scrapers.
        // Order doesn't matter; matching is case-insensitive.
        private static readonly string[] ScraperUserAgents =
        {
        "WhatsApp",
        "facebookexternalhit",
        "Facebot",
        "Twitterbot",
        "LinkedInBot",
        "Slackbot-LinkExpanding",
        "TelegramBot",
        "Discordbot",
        "Pinterest",
        "Applebot",
        "SkypeUriPreview",
    };

        // Only product URLs need this treatment for now.
        // Pattern: /products/{slug}  — slug is anything not "/"
        private static readonly Regex ProductPathRegex =
            new(@"^/products/([^/?#]+)/?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public SocialScraperMiddleware(
            RequestDelegate next,
            IServiceManager service,
            IConfiguration config)
        {
            _next = next;
            _service = service;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!IsScraperRequest(context))
            {
                await _next(context);
                return;
            }

            var match = ProductPathRegex.Match(context.Request.Path.Value ?? "");
            if (!match.Success)
            {
                await _next(context);
                return;
            }

            var slug = WebUtility.UrlDecode(match.Groups[1].Value);
            var product = await _service.ProductService.FindProductBySlugForPreviewAsync(slug);

            if (product == null)
            {
                await _next(context);
                return;
            }

            var html = BuildPreviewHtml(product);

            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/html; charset=utf-8";
            context.Response.Headers["Cache-Control"] = "public, max-age=3600";
            await context.Response.WriteAsync(html, Encoding.UTF8);
        }

        private static bool IsScraperRequest(HttpContext ctx)
        {
            if (!ctx.Request.Headers.TryGetValue("User-Agent", out var ua))
                return false;
            var uaStr = ua.ToString();
            return ScraperUserAgents.Any(s =>
                uaStr.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private string BuildPreviewHtml(ProductPreviewDto p)
        {
            var siteUrl = _config["SiteUrl"]?.TrimEnd('/') ?? "https://buypamoja.com";
            var imageBase = _config["ImageBaseUrl"]?.TrimEnd('/')
                ?? "https://api.ugandaonlinebusiness.com/api/product/images";

            var url = $"{siteUrl}/products/{HtmlEncoder.Default.Encode(p.Slug)}";
            var title = HtmlEncoder.Default.Encode($"{p.ProductName} | Buy Pamoja");
            var desc = HtmlEncoder.Default.Encode(BuildDescription(p));
            var image = !string.IsNullOrEmpty(p.PrimaryImageId)
                ? $"{imageBase}/{p.ProductId}/{p.PrimaryImageId}/large"
                : $"{siteUrl}/og-default.png";

            return $$"""
            <!doctype html>
            <html lang="en">
            <head>
              <meta charset="utf-8">
              <title>{{title}}</title>
              <meta name="description" content="{{desc}}">
              <link rel="canonical" href="{{url}}">

              <meta property="og:type" content="product">
              <meta property="og:site_name" content="Buy Pamoja">
              <meta property="og:title" content="{{title}}">
              <meta property="og:description" content="{{desc}}">
              <meta property="og:image" content="{{image}}">
              <meta property="og:image:width" content="1200">
              <meta property="og:image:height" content="1200">
              <meta property="og:url" content="{{url}}">
              <meta property="og:locale" content="en_UG">

              <meta name="twitter:card" content="summary_large_image">
              <meta name="twitter:title" content="{{title}}">
              <meta name="twitter:description" content="{{desc}}">
              <meta name="twitter:image" content="{{image}}">

              <meta property="product:price:amount" content="{{p.Price}}">
              <meta property="product:price:currency" content="UGX">
            </head>
            <body>
              <h1>{{HtmlEncoder.Default.Encode(p.ProductName)}}</h1>
              <p>{{desc}}</p>
              <p>Price: UGX {{p.Price:N0}}</p>
              <p><a href="{{url}}">View on Buy Pamoja</a></p>
            </body>
            </html>
            """;
        }

        private static string BuildDescription(ProductPreviewDto p)
        {
            var price = $"UGX {p.Price:N0}";
            var body = !string.IsNullOrWhiteSpace(p.Description)
                ? p.Description.Trim()
                : $"{p.ProductName} from {p.SellerName ?? "verified seller"}.";

            var snippet = body.Length > 140 ? body[..140].TrimEnd() + "…" : body;
            return $"{snippet} {price} on Buy Pamoja Uganda.";
        }
    }


}
