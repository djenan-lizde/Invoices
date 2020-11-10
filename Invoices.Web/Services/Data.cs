using AutoMapper;
using Invoices.Web.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Invoices.Web.Services
{
    public class Data<T> : IData<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _entity;
        protected readonly IMapper _mapper;

        public Data(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _entity = context.Set<T>();
            _mapper = mapper;
        }

        public List<T> Get()
        {
            return _entity.AsNoTracking().ToList();
        }
        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate)
        {
            return _entity.AsNoTracking().Where(predicate);
        }
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _entity.FirstOrDefault(predicate);
        }
        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity");
            }

            var x = _entity.Add(entity);
            _context.SaveChanges();
            return x.Entity;
        }
    }
}
