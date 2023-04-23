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
    public class GeneralSettingsManager : GeneralSettingsService
    {
        private readonly IGeneralSettingsRepository _generalSettingsRepository;

        public GeneralSettingsManager(IGeneralSettingsRepository generalSettingsRepository)
        {
            _generalSettingsRepository = generalSettingsRepository;
        }

        public List<GeneralSettings> List(Expression<Func<GeneralSettings, bool>> filter)
        {
            return _generalSettingsRepository.List(filter);
        }

        public void TAdd(GeneralSettings p)
        {
            _generalSettingsRepository.TAdd(p);
        }

        public void TDelete(GeneralSettings p)
        {
            _generalSettingsRepository.TDelete(p);    
        }

        public GeneralSettings TGetById(int id)
        {
            return _generalSettingsRepository.TGetById(id);
        }

        public List<GeneralSettings> TList()
        {
            return _generalSettingsRepository.TList();
        }

        public void TUpdate(GeneralSettings p)
        {
            _generalSettingsRepository.TUpdate(p);
        }
    }
}
