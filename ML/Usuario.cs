﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string UserName { get; set; } = default!;
        public byte[] Password { get; set; } = default!;
        public int Resultado { get; set; }

    }
}
