using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T> where T: class, new()
    {
        List<T> TList();
        void TAdd(T p);
        void TDelete(T p);
        T TGetById(int id);
        void TUpdate(T p);
        List<T> List(Expression<Func<T, bool>> filter);


        

    }
}
