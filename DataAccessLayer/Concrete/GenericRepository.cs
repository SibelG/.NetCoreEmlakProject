using DataAccessLayer.Abstract;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrate
{
    public class GenericRepository<T,TContext> : IRepository<T> where T : class, new() where TContext :DbContext,new() 
    {
        private DataContext _dbContext = new DataContext();
        DbSet<T> _object;
       

        public GenericRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
            _object = _dbContext.Set<T>();  

           
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();

        }

        public void TAdd(T p)
        {
            _object.Add(p);
            _dbContext.SaveChanges();
        }

        public void TDelete(T p)
        {
            _object.Remove(p);  
            _dbContext.SaveChanges();
        }

        public T TGetById(int id)
        {
            return  _object.Find(id);
        }

        public List<T> TList()
        {
            return _object.ToList();
        }

        public void TUpdate(T p)
        {
            _object.Update(p);
            _dbContext.SaveChanges();
        }
    }
}
