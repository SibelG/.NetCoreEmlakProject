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
    public class ImagesManager : ImagesService
    {
        private readonly IImagesRepository _imagesRepository;

        public ImagesManager(IImagesRepository imagesRepository)
        {
            _imagesRepository = imagesRepository;
        }

        public List<Images> List(Expression<Func<Images, bool>> filter)
        {
           return _imagesRepository.List(filter);   
        }

        public void TAdd(Images p)
        {
            _imagesRepository.TAdd(p);
        }

        public void TDelete(Images p)
        {
            _imagesRepository.TDelete(p);
        }

        public Images TGetById(int id)
        {
            return _imagesRepository.TGetById(id);
        }

        public List<Images> TList()
        {
            return _imagesRepository.TList();
        }

        public void TUpdate(Images p)
        {
            var update = _imagesRepository.TGetById(p.ImageId);
            update.ImageName = p.ImageName;
            _imagesRepository.TUpdate(update);
        }
    }
}
