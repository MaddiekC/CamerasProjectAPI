using System;
using System.Collections.Generic;

namespace CamerasProjectAPI.Models;

public partial class Zona
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Estado { get; set; } = null!;

    //public virtual ICollection<Camara> Camaras { get; set; } = new List<Camara>();
}
