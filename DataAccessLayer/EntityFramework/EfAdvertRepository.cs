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
    public class EfAdvertRepository : GenericRepository<Advert,DataContext>, IAdvertRepository
    {
        private DataContext context;
        public EfAdvertRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public void FullDelete(Advert p)
        {
            var advert = context.Adverts.Find(p);
            context.Remove(advert);  
            context.SaveChanges();

        }
    }
}
