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
    public class EfNeighbourhoodRepository : GenericRepository<Neighbourhood,DataContext>, INeighbourhoodRepository
    {
        private DataContext context;
        public EfNeighbourhoodRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
