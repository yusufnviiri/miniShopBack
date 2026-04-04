using Contracts.Repo;
using Entities.Models;
using Repository.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class PaymentRepo : RepositoryBase<Payment>, IPaymentRepo
    {
        public PaymentRepo(ApplicationDbContext _db) : base(_db)
        {

        }
    }
}