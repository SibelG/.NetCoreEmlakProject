using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProjectManager : ProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectManager(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public List<Projects> List(Expression<Func<Projects, bool>> filter)
        {
           return _projectRepository.List(filter);
        }

        public void TAdd(Projects p)
        {
            p.Status = true;
            _projectRepository.TAdd(p);  
        }

        public void TDelete(Projects p)
        {
            _projectRepository.TDelete(p);   
        }

        public Projects TGetById(int id)
        {
            return _projectRepository.TGetById(id); 
        }

        public List<Projects> TList()
        {
            return _projectRepository.TList();  
        }

        public void TUpdate(Projects p)
        {
            _projectRepository.TUpdate(p);   
        }
    }
}
