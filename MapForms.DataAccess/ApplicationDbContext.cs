using MapForms.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapForms.DataAccess
{
    public class ApplicationDbContext: IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions options) : base (options)
        {

        }
    }
}
