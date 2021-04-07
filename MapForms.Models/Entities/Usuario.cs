using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MapForms.Models.Entities
{
    public class Usuario : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsDriver { get; set; } 
    }
}
