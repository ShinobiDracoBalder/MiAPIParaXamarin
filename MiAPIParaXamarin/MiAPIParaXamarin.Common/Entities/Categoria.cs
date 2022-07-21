using System.ComponentModel.DataAnnotations;

namespace MiAPIParaXamarin.Common.Entities
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Descripcion es obligatorio")]
        public string Descripcion { get; set; }
    }
}
