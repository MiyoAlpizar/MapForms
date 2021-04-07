using AutoMapper;
using MapForms.DataAccess.Data.Repositories;
using MapForms.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapForms.DataAccess.Data
{
    public class WorkContainer : IWorkContainer
    {
        public IUsuarioRepository Usuarios { get; private set; }

        public WorkContainer(ApplicationDbContext context, IMapper mapper, SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, IConfiguration configuration)
        {
            Usuarios = new UsuarioRepository(context, mapper, signInManager, userManager, configuration);
        }
    }
}
