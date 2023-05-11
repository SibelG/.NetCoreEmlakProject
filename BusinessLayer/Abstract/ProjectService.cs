using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ProjectService: GenericService<Projects>
    {
        public void RestoreDelete(Projects projects);
        public void FullDelete(Projects projects);
    }
}
