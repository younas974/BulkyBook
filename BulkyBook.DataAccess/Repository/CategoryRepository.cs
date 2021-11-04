using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
  public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)  : base(db)
        {
            _db = db;
        }

        public void Updae(Category category)
        {
            var objFromDb = _db.categories.FirstOrDefault(s=> s.Id==category.Id);

            if (objFromDb != null) {
                objFromDb.Name = category.Name;
     
            }
           
        }
    }
}
