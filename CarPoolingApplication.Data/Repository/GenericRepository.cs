using CarPoolingApplication.Data;
using CarPoolingApplication.Models;
using CarPoolingApplication.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPoolingApplication.Data;

namespace CarPoolingApplication.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T:class
    {
        private DataContext _dataContext;
        private DbSet<T> dbSet;
        public GenericRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            dbSet = _dataContext.Set<T>();
        }
        public async Task<List<T>> GetData()
        {
            var data = await dbSet.ToListAsync();
            return data;
        }
        public async Task<T> GetDataById(int id)
        {
            var data = await dbSet.FindAsync(id);

            return data;
        }
       /* public async void UpdateData(T data, int id)
        {
            dbSet.Attach(data);
            _dataContext.Entry(data).State = EntityState.Modified;

        }*/
        public async void AddData(T data)
        {
            await dbSet.AddAsync(data);
            _dataContext.SaveChanges();
        }


    }
}
