﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PienCeramic.DataAccess.Repository.IRepository
{
    public interface IUnitofWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        void Save();
    }
}
