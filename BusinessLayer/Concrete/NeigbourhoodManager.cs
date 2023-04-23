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
    public class NeigbourhoodManager : NeighbourhoodService
    {
        private readonly INeighbourhoodRepository _neighbourhoodRepository;
        public NeigbourhoodManager(INeighbourhoodRepository neighbourhoodRepository)
        {
            _neighbourhoodRepository = neighbourhoodRepository;
        }

        public List<Neighbourhood> List(Expression<Func<Neighbourhood, bool>> filter)
        {
           return  _neighbourhoodRepository.List(filter);  
        }

        public void TAdd(Neighbourhood p)
        {
            _neighbourhoodRepository.TAdd(p);
        }

        public void TDelete(Neighbourhood p)
        {
           _neighbourhoodRepository.TDelete(p);
        }

        public Neighbourhood TGetById(int id)
        {
            return _neighbourhoodRepository.TGetById(id);
        }

        public List<Neighbourhood> TList()
        {
            return _neighbourhoodRepository.TList();
        }

        public void TUpdate(Neighbourhood p)
        {
            _neighbourhoodRepository.TUpdate(p);
        }
    }
}
