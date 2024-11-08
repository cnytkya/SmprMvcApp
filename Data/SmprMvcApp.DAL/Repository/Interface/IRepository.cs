﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmprMvcApp.DAL.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        //T=>Category
        IEnumerable<T> GetAll();

        T Get(Expression<Func<T, bool>> filter);

        //void Update(T entity);
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);
    }
}