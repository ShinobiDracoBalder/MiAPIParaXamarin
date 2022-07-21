using MiAPIParaXamarin.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace MiAPIParaXamarin.DataBase
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public virtual DbSet<Categoria> Categorias { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }
    }
}
