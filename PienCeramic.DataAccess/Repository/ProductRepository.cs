using PienCeramic.Data;
using PienCeramic.DataAccess.Repository.IRepository;
using PienCeramic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PienCeramic.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        void IProductRepository.Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
