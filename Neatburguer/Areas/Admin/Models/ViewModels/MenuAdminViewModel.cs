namespace Neatburguer.Areas.Admin.Models.ViewModels
{
    public class MenuAdminViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public double Precio { get; set; }
        public double PrecioPromocion { get; set; }

        public string Clasificacion { get; set; } = null!;

    }
}
