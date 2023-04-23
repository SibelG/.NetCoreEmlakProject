using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfTypeRepository : GenericRepository<EntityLayer.Entities.Type,DataContext>, ITypeRepository
    {
        private DataContext context;
        public EfTypeRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
