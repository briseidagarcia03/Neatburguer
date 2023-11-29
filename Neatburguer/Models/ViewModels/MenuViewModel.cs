namespace Neatburguer.Models.ViewModels
{
    //public class MenuViewModel
    //{
    //    public ModelHamburguesas Hamburguesa { get; set; }
    //    public List<ModelHamburguesas> Hamburguesas { get; set;} = new List<ModelHamburguesas>();
    //}
    public class MenuViewModel
    {
        public ModelHamburguesas Hamburguesa { get; set; } = null!;
        public IEnumerable<IGrouping<string, ModelHamburguesas>> Hamburguesas { get; set;} = null!;
    }

    public class ModelHamburguesas
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public double Precio { get; set; }
        public double PrecioPromocion { get; set; }

        public string Clasificacion { get; set; } = null!;
        
    }
}
