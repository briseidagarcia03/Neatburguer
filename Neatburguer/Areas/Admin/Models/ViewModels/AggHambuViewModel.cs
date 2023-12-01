namespace Neatburguer.Areas.Admin.Models.ViewModels
{
    public class AggHambuViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Descripcion { get; set; } = null!;
        public IFormFile? Img { get; set; } = null!;
            
        public int IdClasificacion { get; set; }
        public IEnumerable<ClasificacionModel>? ListaClasificaciones { get; set; }

    }

    public class ClasificacionModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
