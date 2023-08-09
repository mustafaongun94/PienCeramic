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
    public class CompanyRepository: Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        void ICompanyRepository.Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
