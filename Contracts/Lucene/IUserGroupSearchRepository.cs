using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Lucene
{
    public interface IUserGroupSearchRepository
    {
        void AddOrUpdate(UserGroupIndexDocument document);
        void Delete(Guid userGroupId);
        void Commit();

        UserGroupSearchResult Search(UserGroupSearchRequest request, int maxPageSize);
    }
}
