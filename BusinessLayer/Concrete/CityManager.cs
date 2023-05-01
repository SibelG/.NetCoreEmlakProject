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
    public class CityManager : CityService
    {
        private readonly ICityRepository _cityRepository;

        public CityManager(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public List<City> List(Expression<Func<City, bool>> filter)
        {
            return _cityRepository.List(filter);   
        }

        public void TAdd(City p)
        {
            p.Status = true;
            _cityRepository.TAdd(p);
        }

        public void TDelete(City p)
        {
            var city = _cityRepository.TGetById(p.CityId);
            city.Status = false;
            _cityRepository.TUpdate(city);
        }

        public City TGetById(int id)
        {
            return _cityRepository.TGetById(id);
        }

        public List<City> TList()
        {
            return _cityRepository.TList();
        }

        public void TUpdate(City p)
        {
            var city = _cityRepository.TGetById(p.CityId);
            city.CityName = p.CityName;
            _cityRepository.TUpdate(city);
        }
    }
}
