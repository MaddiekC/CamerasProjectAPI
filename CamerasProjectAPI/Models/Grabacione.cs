using System;
using System.Collections.Generic;

namespace CamerasProjectAPI.Models;

public partial class Grabacione
{
    public int Id { get; set; }

    public int IdCamara { get; set; }

    public DateTime InicioGrabacion { get; set; }

    public DateTime FinGrabacion { get; set; }

    public string UbicacionArchivo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Estado { get; set; } = null!;

    //public virtual Camara IdCamaraNavigation { get; set; } = null!;
}
