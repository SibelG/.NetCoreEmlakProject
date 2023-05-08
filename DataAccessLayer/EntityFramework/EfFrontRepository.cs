using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfFrontRepository : GenericRepository<Front, DataContext>, IFrontRepository
    {
        private DataContext context;
        public EfFrontRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
