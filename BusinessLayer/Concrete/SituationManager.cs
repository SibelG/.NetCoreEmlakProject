using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class SituationManager : SituationService
    {
        private readonly ISituationRepository _situationRepository;

        public SituationManager(ISituationRepository situationRepository)
        {
            _situationRepository = situationRepository;
        }

        public List<Situation> List(Expression<Func<Situation, bool>> filter)
        {
            return _situationRepository.List(filter);   
        }

        public void TAdd(Situation p)
        {
            _situationRepository.TAdd(p);   
        }

        public void TDelete(Situation p)
        {
            _situationRepository.TDelete(p);
        }

        public Situation TGetById(int id)
        {
            return _situationRepository.TGetById(id);
        }

        public List<Situation> TList()
        {
            return _situationRepository.TList();
        }

        public void TUpdate(Situation p)
        {
            _situationRepository.TUpdate(p);
        }
    }
}
