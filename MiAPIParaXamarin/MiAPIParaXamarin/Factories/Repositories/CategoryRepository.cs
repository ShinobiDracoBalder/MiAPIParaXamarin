using MiAPIParaXamarin.Common.Entities;
using MiAPIParaXamarin.DataBase;
using MiAPIParaXamarin.Factories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiAPIParaXamarin.Factories.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> AddAsync(Categoria entity)
        {
            _dataContext.Categorias.Add(entity);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var avatar = await _dataContext
                .Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);
            _dataContext.Categorias.Remove(avatar);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }

        public async Task<int> DeleteCategoriaAsync(int id)
        {
            var avatar = await _dataContext.Categorias
                .FirstOrDefaultAsync(c => c.CategoriaId == id);
           
            _dataContext.Categorias.Remove(avatar);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }

        public async Task<IReadOnlyList<Categoria>> GetAllAsync()
        {
            return await _dataContext.Categorias.ToListAsync();
        }

        public async Task<List<Categoria>> GetAllTblCategoriaAsync()
        {
           return await _dataContext.Categorias.ToListAsync(); 
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            return await _dataContext.Categorias.FirstOrDefaultAsync(c => c.CategoriaId.Equals(id));
        }

        public async Task<Categoria> GetOnlyTblCategoriaAsync(int id)
        {
            return await _dataContext.Categorias
                .FirstOrDefaultAsync(c => c.CategoriaId.Equals(id));
        }

        public async Task<int> UpdateAsync(Categoria entity)
        {
            _dataContext.Categorias.Update(entity);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }
        private async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
