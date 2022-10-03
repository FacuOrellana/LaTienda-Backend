using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP1IdS_G15Application.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string IPv4 { get; set; }
    }
    public class LoginResponse
    {
        public string Token { get; set; }
        public string TipoUsuario { get; set; }
        public int SessionId { get; set; }
    }
}