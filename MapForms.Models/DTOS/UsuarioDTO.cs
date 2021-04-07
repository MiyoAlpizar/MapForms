using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapForms.Models.DTOS
{
    public class UsuarioDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }
    }

    public class UsuarioCreateDTO
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }


    }

    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UsuarioToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
