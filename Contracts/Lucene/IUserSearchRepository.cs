using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    public interface IUserSearchRepository
    {
        void AddOrUpdate(UserIndexDocument document);
        void Delete(Guid userProfileId);
        void Commit();

        UserSearchResult Search(UserSearchRequest request, int maxPageSize);
    }
}
