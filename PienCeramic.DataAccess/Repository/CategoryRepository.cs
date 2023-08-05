using PienCeramic.Data;
using PienCeramic.DataAccess.Repository.IRepository;
using PienCeramic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PienCeramic.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        

        void ICategoryRepository.Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
