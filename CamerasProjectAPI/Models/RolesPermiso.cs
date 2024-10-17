using System;
using System.Collections.Generic;

namespace CamerasProjectAPI.Models;

public partial class RolesPermiso
{
    public int Id { get; set; }

    public int IdRoles { get; set; }

    public int IdPermisos { get; set; }
}
