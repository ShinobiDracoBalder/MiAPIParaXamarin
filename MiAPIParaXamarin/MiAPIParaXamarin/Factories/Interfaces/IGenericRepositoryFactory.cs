﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiAPIParaXamarin.Factories.Interfaces
{
    public interface IGenericRepositoryFactory<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}
