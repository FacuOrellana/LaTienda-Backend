using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1IdS_G15Modelo.Entidades
{
    public enum TipoUsuario
    {
        Vendedor,
        Administrador
    }
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
