using System;
using System.Collections.Generic;

namespace CamerasProjectAPI.Models;

public partial class Camara
{
    public int Id { get; set; }

    public string Ip { get; set; } = null!;

    public int IdZona { get; set; }

    public string Nombre { get; set; } = null!;

    public string Estado { get; set; } = null!;

    /*public virtual ICollection<Grabacione> Grabaciones { get; set; } = new List<Grabacione>();

    public virtual Zona IdZonaNavigation { get; set; } = null!;*/
}
