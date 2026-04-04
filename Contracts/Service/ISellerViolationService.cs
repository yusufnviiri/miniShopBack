using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface ISellerViolationService
    {

        Task<IEnumerable<SellerViolation>> GetAllSellerViolations();
        Task<SellerViolation?> FindSellerViolationById(Guid violationId, bool tracking);
        Task CreateSellerViolation(SellerViolation violation);
        Task UpdateSellerViolation(SellerViolation violation);
        Task DeleteSellerViolation(Guid violationId);
    }
}
