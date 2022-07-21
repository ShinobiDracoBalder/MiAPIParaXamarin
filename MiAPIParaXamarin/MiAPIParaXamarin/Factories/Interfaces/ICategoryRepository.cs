using MiAPIParaXamarin.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiAPIParaXamarin.Factories.Interfaces
{
    public interface ICategoryRepository : IGenericRepositoryFactory<Categoria>
    {
        Task<List<Categoria>> GetAllTblCategoriaAsync();
        Task<Categoria> GetOnlyTblCategoriaAsync(int id);
        Task<int> DeleteCategoriaAsync(int id);
        Task<bool> ExisteCategoriaAsync(string nombre);
        Task<bool> ExisteCategoriaAsync(int id);
    }
}
