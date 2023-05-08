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
    public class EfHeadingRepository : GenericRepository<Heading, DataContext>, IHeadingRepository
    {
        private DataContext context;
        public EfHeadingRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
   
}
