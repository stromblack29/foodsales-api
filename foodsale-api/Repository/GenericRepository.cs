using foodsale_api.Context;
using foodsale_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace foodsale_api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected FoodContext _context = null;
        protected DbSet<T> table = null;
        public GenericRepository(FoodContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            if (existing != null)
            {
                table.Remove(existing);
            }
        }

        public IQueryable<T> GetAll()
        {
            return table.AsQueryable();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
