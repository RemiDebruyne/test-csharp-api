using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Stereograph.TechnicalTest.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Stereograph.TechnicalTest.Api.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public Task<bool> Delete(int id);
        public Task<T> Get(Expression<Func<T, bool>> predicate);
        public Task<List<T>> GetAll();
        public Task<List<T>> GetAll(Expression<Func<Person, bool>> predicate);
    }
}
