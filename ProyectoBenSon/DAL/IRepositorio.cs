﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBenSon.DAL
{
    interface IRepositorio<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T[]> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
