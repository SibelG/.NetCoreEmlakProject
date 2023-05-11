using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IProjectRepository : IRepository<Projects>
    {
        public void FullDelete(Projects p);
    }
}
