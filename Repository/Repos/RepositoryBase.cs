using Contracts.Repo;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public abstract class RepositoryBase<T>:IRepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        protected RepositoryBase(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }
        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? _context.Set<T>()
   .AsNoTracking() : _context.Set<T>();
        public  IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,bool trackChanges)=> !trackChanges? _context.Set<T>().Where(expression).AsNoTracking() :
                _context.Set<T>().Where(expression);
        
       public void CreateBase(T entity)=> _context.Set<T>().Add(entity);

        public void UpdateBase(T entity)=> _context.Set<T>().Update(entity);
      public  void DeleteBase(T entity)=> _context.Set<T>().Remove(entity);
    }
}
