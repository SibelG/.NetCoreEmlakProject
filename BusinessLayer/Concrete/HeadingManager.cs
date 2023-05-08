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
    public class HeadingManager : HeadingService
    {
        private readonly IHeadingRepository _headingRepository;

        public HeadingManager(IHeadingRepository headingRepository)
        {
            _headingRepository = headingRepository;
        }

        public List<Heading> List(Expression<Func<Heading, bool>> filter)
        {
            return _headingRepository.List(filter);
        }

        public void TAdd(Heading p)
        {
            _headingRepository.TAdd(p);
        }

        public void TDelete(Heading p)
        {
            _headingRepository.TDelete(p);
        }

        public Heading TGetById(int id)
        {
            return _headingRepository.TGetById(id);
        }

        public List<Heading> TList()
        {
            return _headingRepository.TList();
        }

        public void TUpdate(Heading p)
        {
            _headingRepository.TUpdate(p);
        }
    }
}
