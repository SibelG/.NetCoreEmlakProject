using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface GenericService<T> where T : class

    {
        List<T> TList();
        void TAdd(T p);
        void TDelete(T p);
        T TGetById(int id);
        void TUpdate(T p);
        List<T> List(Expression<Func<T, bool>> filter);
    }
}
