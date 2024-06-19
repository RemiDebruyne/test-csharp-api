using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stereograph.TechnicalTest.Api.Entities;
using Stereograph.TechnicalTest.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Stereograph.TechnicalTest.Api.Repositories
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Person> Add(Person entity)
        {
            var addedEntry = await _context.Persons.AddAsync(entity);

            await _context.SaveChangesAsync();

            if (addedEntry.Entity.Id > 0)
                return addedEntry.Entity;

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var entityToDelete = await _context.FindAsync<Person>(id);

            if (entityToDelete == null)
                return false;

            _context.Persons.Remove(entityToDelete);

            return await _context.SaveChangesAsync() > 0;


        }

        public async Task<Person> Get(Expression<Func<Person, bool>> predicate)
        {
            return await _context.Persons.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Person>> GetAll()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<List<Person>> GetAll(Expression<Func<Person, bool>> predicate)
        {
            return await _context.Persons.Where(predicate).ToListAsync();
        }

        public async Task<Person> Update(Person entity)
        {
            var entityFromDb = await Get(e => e.Id == entity.Id);

            Type entityType = entity.GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            foreach (var prop in entityProperties)
            {
                var propertyValue = prop.GetValue(entity, null);
                var propertyValueFromDb = prop.GetValue(entityFromDb, null);

                if (propertyValueFromDb != propertyValue)
                    propertyValueFromDb = propertyValue;
            }

            if (await _context.SaveChangesAsync() == 0)
                return null;

            return entityFromDb;
        }
    }
}
