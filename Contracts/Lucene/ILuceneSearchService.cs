using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;
using Shared.Lucene;

namespace Contracts.Lucene
{
    public interface ILuceneSearchService
    {
        ProductSearchResponse Search(ProductSearchRequest request);

        HomePageCustomProductsDto SearchHomePage(string searchQuery);


    }
}
