using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
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
    public class DistrictManager : DistrictService
    {
        private readonly IDistrictRepository _districtRepository;

        public DistrictManager(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public List<District> List(Expression<Func<District, bool>> filter)
        {
            return _districtRepository.List(filter);   
        }

        public void TAdd(District p)
        {
            p.Status = true;
            _districtRepository.TAdd(p);
        }

        public void TDelete(District p)
        {
            var district = _districtRepository.TGetById(p.DistrictId);
            district.Status = false;
            _districtRepository.TUpdate(district);
            
        }

        public District TGetById(int id)
        {
            return _districtRepository.TGetById(id);
        }

        public List<District> TList()
        {
            return _districtRepository.TList();
        }

        public void TUpdate(District p)
        {
            _districtRepository.TUpdate(p);
        }
    }
}
