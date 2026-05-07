using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    public interface ITradeSearchRepository
    {
        void AddOrUpdate(TradeIndexDocument document);
        void Delete(Guid tradeId);
        void DeleteBySeller(Guid sellerProfileId);
        void Commit();

        TradeSearchResult Search(TradeSearchRequest request, int maxPageSize);
    }
}
