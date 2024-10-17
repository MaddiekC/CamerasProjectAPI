using System;
using System.Collections.Generic;

namespace CamerasProjectAPI.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdRol { get; set; }

    public string Estado { get; set; } = null!;
}
