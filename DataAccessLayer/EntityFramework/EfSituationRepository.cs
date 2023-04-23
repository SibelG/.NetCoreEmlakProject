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
    public class EfSituationRepository : GenericRepository<Situation,DataContext>, ISituationRepository
    {
        private DataContext context;
        public EfSituationRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
