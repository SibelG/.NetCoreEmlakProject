using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
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

        public void FullDelete(Projects p)
        {
            var delete = _projectRepository.TGetById(p.ProjectId);
            _projectRepository.FullDelete(delete);
        }

        public List<Projects> List(Expression<Func<Projects, bool>> filter)
        {
           return _projectRepository.List(filter);
        }

        public void RestoreDelete(Projects p)
        {

            var delete = _projectRepository.TGetById(p.ProjectId);
            p.Status = true;
            _projectRepository.TUpdate(p);
        }

        public void TAdd(Projects p)
        {
            p.Status = true;
            _projectRepository.TAdd(p);  
        }

        public void TDelete(Projects p)
        {
           
            var delete = _projectRepository.TGetById(p.ProjectId);
            p.Status = false;
            _projectRepository.TUpdate(p);
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
            var project = _projectRepository.TGetById(p.ProjectId);
            project.Address = p.Address;
            project.Description = p.Description;
            project.ProjectTitle = p.ProjectTitle;
            project.ProjectCompany = p.ProjectCompany;
            project.NumberOfRooms = p.NumberOfRooms;
            project.CarPark = p.CarPark;
            project.BuildingDelivery = p.BuildingDelivery;
            project.DeliveryDate = p.DeliveryDate;
            project.CategoryId = p.CategoryId;
            project.TypeCount = p.TypeCount;
            project.Elevator = p.Elevator;
            project.CityId = p.CityId;
            project.NeighbourhoodId = p.NeighbourhoodId;
            project.TypeId = p.TypeId;
            project.HeadingId = p.HeadingId;
            project.DistrictId = p.DistrictId;
            project.FloorCount = p.FloorCount;
            project.Area = p.Area;

            _projectRepository.TUpdate(p);   
        }
    }
}
