using BLL.Interface;

using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Specifications;

namespace BLL.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepo(StoreContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
             => await _context.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecifications(spec).ToListAsync();


        public async Task<T> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<int> GetCountAsync(ISpecification<T> spec)
            => await ApplySpecifications(spec).CountAsync();

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
            => await ApplySpecifications(spec).FirstOrDefaultAsync();

        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }


        public async void Add(T entity)
        {
            _context.Set<T>().Add(entity);
              await  _context.SaveChangesAsync();
                }
        public async void Update(T entity)
        {

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        public async void Delete(T entity)
        { 
             _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
