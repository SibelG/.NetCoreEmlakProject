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
    public class FrontManager : FrontService
    {
        private readonly IFrontRepository _frontRepository;

        public FrontManager(IFrontRepository frontRepository)
        {
            _frontRepository = frontRepository;
        }

        public List<Front> List(Expression<Func<Front, bool>> filter)
        {
            return _frontRepository.List(filter);
        }

        public void TAdd(Front p)
        {
             _frontRepository.TAdd(p);
        }

        public void TDelete(Front p)
        {
            _frontRepository.TDelete(p);
        }

        public Front TGetById(int id)
        {
            return _frontRepository.TGetById(id);
        }

        public List<Front> TList()
        {
            return _frontRepository.TList();
        }

        public void TUpdate(Front p)
        {
            _frontRepository.TUpdate(p);
        }
    }
}
