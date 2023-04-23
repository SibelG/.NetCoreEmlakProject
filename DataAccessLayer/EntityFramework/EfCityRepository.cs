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
    public class EfCityRepository : GenericRepository<City,DataContext>, ICityRepository
    {
        private DataContext context;
        public EfCityRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
