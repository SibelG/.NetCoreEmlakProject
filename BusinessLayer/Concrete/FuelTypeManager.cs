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
    public class FuelTypeManager:FuelTypeService
    {
        private readonly IFuelTypeRepository _fuelTypeRepository;
        public FuelTypeManager(IFuelTypeRepository repository)
        {
            _fuelTypeRepository = repository;
        }

        List<FuelType> GenericService<FuelType>.List(Expression<Func<FuelType, bool>> filter)
        {
            return _fuelTypeRepository.List(filter);
        }

        void GenericService<FuelType>.TAdd(FuelType p)
        {
            _fuelTypeRepository.TAdd(p);
        }

        void GenericService<FuelType>.TDelete(FuelType p)
        {
            _fuelTypeRepository.TDelete(p);
        }

        FuelType GenericService<FuelType>.TGetById(int id)
        {
            return _fuelTypeRepository.TGetById(id);
        }

        List<FuelType> GenericService<FuelType>.TList()
        {
            return _fuelTypeRepository.TList();
        }

        void GenericService<FuelType>.TUpdate(FuelType p)
        {
            _fuelTypeRepository.TUpdate(p);
        }
    }
}
