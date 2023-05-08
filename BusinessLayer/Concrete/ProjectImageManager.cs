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

    public class ProjectImageManager: ProjectImageService
    {
        private readonly IProjectImageRepository _projectImageRepository;
         public ProjectImageManager(IProjectImageRepository projectImageRepository)
         {
             _projectImageRepository = projectImageRepository;
         }

         public List<ProjectImage> List(Expression<Func<ProjectImage, bool>> filter)
         {
             return _projectImageRepository.List(filter);
         }

         public void TAdd(ProjectImage p)
         {
             _projectImageRepository.TAdd(p);
         }

         public void TDelete(ProjectImage p)
         {
             _projectImageRepository.TDelete(p);
         }

         public ProjectImage TGetById(int id)
         {
             return _projectImageRepository.TGetById(id);
         }

         public List<ProjectImage> TList()
         {
             return _projectImageRepository.TList();
         }

         public void TUpdate(ProjectImage p)
         {
             _projectImageRepository.TUpdate(p);
         }
     
    }
}
