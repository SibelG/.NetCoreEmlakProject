using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public List<Category> List(Expression<Func<Category, bool>> filter)
        {
            return _categoryRepository.List(filter);
        }

        public void TAdd(Category p)
        {
            _categoryRepository.TAdd(p);
        }

        public void TDelete(Category p)
        {
            _categoryRepository.TDelete(p);
        }

        public Category TGetById(int id)
        {
            return _categoryRepository.TGetById(id);
        }

        public List<Category> TList()
        {
            return _categoryRepository.TList();
        }

        public void TUpdate(Category p)
        {
            _categoryRepository.TUpdate(p);
        }
    }
    
}
