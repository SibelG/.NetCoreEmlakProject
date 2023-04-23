using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface AdvertService: GenericService<Advert>
    {
        public void RestoreDelete(Advert advert);
        public void FullDelete(Advert advert);
    }
}
