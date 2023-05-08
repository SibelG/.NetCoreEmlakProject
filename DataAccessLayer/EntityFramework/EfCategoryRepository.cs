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
    public class EfCategoryRepository :GenericRepository<Category, DataContext>, ICategoryRepository
    {
        private DataContext context;
        public EfCategoryRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
