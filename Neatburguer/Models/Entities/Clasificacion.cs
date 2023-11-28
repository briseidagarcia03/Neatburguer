using System;
using System.Collections.Generic;

namespace Neatburguer.Models.Entities;

public partial class Clasificacion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Menu> Menu { get; set; } = new List<Menu>();
}
