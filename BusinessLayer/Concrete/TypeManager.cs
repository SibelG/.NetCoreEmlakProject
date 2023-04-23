using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TypeManager : TypeService
    {
        private readonly ITypeRepository _typeRepository;

        public TypeManager(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public List<EntityLayer.Entities.Type> List(Expression<Func<EntityLayer.Entities.Type, bool>> filter)
        {
            return _typeRepository.List(filter); 
        }

        public void TAdd(EntityLayer.Entities.Type p)
        {
            _typeRepository.TAdd(p);
        }

        public void TDelete(EntityLayer.Entities.Type p)
        {
            _typeRepository.TDelete(p);
        }

        public EntityLayer.Entities.Type TGetById(int id)
        {
            return _typeRepository.TGetById(id);
        }

        public List<EntityLayer.Entities.Type> TList()
        {
            return _typeRepository.TList();
        }

        public void TUpdate(EntityLayer.Entities.Type p)
        {
            _typeRepository.TUpdate(p);
        }
    }
}
