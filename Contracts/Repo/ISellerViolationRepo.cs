using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface ISellerViolationRepo
    {
        Task<IEnumerable<SellerViolation>> GetAllSellerViolations();
        Task<SellerViolation?> FindSellerViolationById(Guid violationId, bool tracking);
        void CreateSellerViolation(SellerViolation violation);
        void UpdateSellerViolation(SellerViolation violation);
        void DeleteSellerViolation(SellerViolation violation);
    }
}
