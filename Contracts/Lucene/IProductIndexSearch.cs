using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    public interface IProductIndexSearch
    {
        void IndexProduct(Product product);
        void IndexProducts(IEnumerable<Product> products);
        void UpdateProduct(Product product);
        void DeleteProduct(Guid productId);
        void RebuildIndex(IEnumerable<Product> products);
    }
}
