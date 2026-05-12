using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    internal static class CacheKeys
    {
        public static string ProductById(Guid id) => $"product:id:{id}";
        public static string ProductBySlug(string slug) => $"product:slug:{slug}";
        public static string GroupProducts(Guid groupId) => $"group:{groupId}:products";
        public static string GroupMembersProducts(string hash) => $"group_members:{hash}";
        public static string HomeFeed => "home:custom_products";
        public static string HomePageProducts(string h) => $"home:list:{h}";

        // for cascading invalidation
        public const string AnyProduct = "product:";
        public const string AnyHome = "home:";
    }
}
