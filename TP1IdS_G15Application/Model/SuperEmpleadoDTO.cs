using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15Application.Model
{
    public class SuperEmpleadoDTO
    {
        public int Legajo { get; set; }
        public string ApellidoYNombre { get; set; }
        public string UserName { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
