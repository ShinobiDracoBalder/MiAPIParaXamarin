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
            _dataContext.Categoria.Add(entity);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var avatar = await _dataContext
                .Categoria.FirstOrDefaultAsync(c => c.CategoriaId == id);
            _dataContext.Categoria.Remove(avatar);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }

        public async Task<int> DeleteCategoriaAsync(int id)
        {
            var avatar = await _dataContext.Categoria
                .FirstOrDefaultAsync(c => c.CategoriaId == id);
           
            _dataContext.Categoria.Remove(avatar);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }

        public async Task<IReadOnlyList<Categoria>> GetAllAsync()
        {
            return await _dataContext.Categoria.ToListAsync();
        }

        public async Task<List<Categoria>> GetAllTblCategoriaAsync()
        {
           return await _dataContext.Categoria.ToListAsync(); 
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            return await _dataContext.Categoria.FirstOrDefaultAsync(c => c.CategoriaId.Equals(id));
        }

        public async Task<Categoria> GetOnlyTblCategoriaAsync(int id)
        {
            return await _dataContext.Categoria
                .FirstOrDefaultAsync(c => c.CategoriaId.Equals(id));
        }

        public async Task<int> UpdateAsync(Categoria entity)
        {
            _dataContext.Categoria.Update(entity);
            var r = await SaveAllAsync();
            return r == true ? 1 : 0;
        }
        public async Task<bool> ExisteCategoriaAsync(string nombre)
        {
            bool valor = await _dataContext.Categoria.AnyAsync(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public async Task<bool> ExisteCategoriaAsync(int id)
        {
            return await _dataContext.Categoria.AnyAsync(c => c.CategoriaId == id);
        }
        private async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
