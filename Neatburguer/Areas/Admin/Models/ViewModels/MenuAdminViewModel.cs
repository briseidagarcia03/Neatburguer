namespace Neatburguer.Areas.Admin.Models.ViewModels
{
    public class MenuAdminViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public decimal PrecioPromocion { get; set; }

        public string Clasificacion { get; set; } = null!;

    }
}
