using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfGeneralSettingsRepository : GenericRepository<GeneralSettings,DataContext>, IGeneralSettingsRepository
    {
        private DataContext context;
        public EfGeneralSettingsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
