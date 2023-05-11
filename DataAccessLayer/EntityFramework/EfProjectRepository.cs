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
    public class EfProjectRepository : GenericRepository<Projects, DataContext>, IProjectRepository
    {
        private DataContext context;
        public EfProjectRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
        public void FullDelete(Projects p)
        {
            var project = context.Projects.Find(p);
            context.Remove(project);
            context.SaveChanges();

        }
    }
}
