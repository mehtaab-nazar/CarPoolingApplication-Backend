using CarPoolingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Data.Interfaces
{
    public interface IGenericRepository<T> where T:class
    {
        Task<T> GetDataById(int id);
      //  void UpdateData(T data, int id);
        void AddData(T data);
        Task<List<T>> GetData();
    }
}
