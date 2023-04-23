﻿using BusinessLayer.Abstract;
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
    public class AdvertManager : AdvertService
    {
        private readonly IAdvertRepository _advertRepository;

        public AdvertManager(IAdvertRepository advertRepository)
        {
            _advertRepository = advertRepository;
        }

        public void FullDelete(Advert p)
        {
            var delete = _advertRepository.TGetById(p.AdvertId);
            _advertRepository.FullDelete(delete);   
        }

        public List<Advert> List(Expression<Func<Advert, bool>> filter)
        {
            return _advertRepository.List(filter);
        }

        public void RestoreDelete(Advert p)
        {
            var delete = _advertRepository.TGetById(p.AdvertId);
            p.Status = true;
            _advertRepository.TUpdate(p);
        }

        public void TAdd(Advert p)
        {
            p.Status = true;
            p.AdvertDate = DateTime.Now;
            _advertRepository.TAdd(p);
        }

        public void TDelete(Advert p)
        {
            var delete = _advertRepository.TGetById(p.AdvertId);
            p.Status = false;
            _advertRepository.TUpdate(p);  
        }

        public Advert TGetById(int id)
        {
            return _advertRepository.TGetById(id);
        }

        public List<Advert> TList()
        {
            return _advertRepository.TList();
        }

        public void TUpdate(Advert p)
        {
            _advertRepository.TUpdate(p);
        }
    }
}
