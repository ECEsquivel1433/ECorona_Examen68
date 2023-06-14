using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string UserName { get; set; } = default!;

    public byte[]? Password { get; set; }

    public virtual ICollection<Historial> Historials { get; set; } = new List<Historial>();
}
