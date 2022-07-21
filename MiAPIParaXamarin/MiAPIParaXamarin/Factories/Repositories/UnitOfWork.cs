using MiAPIParaXamarin.Factories.Interfaces;

namespace MiAPIParaXamarin.Factories.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ICategoryRepository _categoryRepository { get; }
    }
}
