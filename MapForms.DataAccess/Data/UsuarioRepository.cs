using AutoMapper;
using MapForms.DataAccess.Data.Repositories;
using MapForms.Models.DTOS;
using MapForms.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MapForms.DataAccess.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly SignInManager<Usuario> signInManager;
        private readonly UserManager<Usuario> userManager;
        private readonly IConfiguration configuration;

        public UsuarioRepository(ApplicationDbContext context, IMapper mapper, SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetUsuariosList()
        {
            var users = await context.Users.ToListAsync();
            return mapper.Map<List<UsuarioDTO>>(users);
        }

        public async Task<ApiResponse<UsuarioToken>> LoginUser(UserLogin userInfo)
        {
            var result = await signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var usuario = await userManager.FindByEmailAsync(userInfo.Email);
                var roles = await userManager.GetRolesAsync(usuario);
                var token = await BuildToken(userInfo, roles);
                return new ApiResponse<UsuarioToken> { Result = token };
            }
            else
            {
                return new ApiResponse<UsuarioToken> { Result = null, StatusResponse = StatusResponse.NotFound , MessageError = "Usuario no encontrado con las credenciales establecidas.", StatusCode = 404 };
            }
        }

        public async Task<ApiResponse<UsuarioToken>> RegisterUser(UsuarioCreateDTO dTO)
        {
            var user = new Usuario { UserName = dTO.Email, FirstName = dTO.Name, Email = dTO.Email, };
            var result = await userManager.CreateAsync(user, dTO.Password);
            if (result.Succeeded)
            {
                var token = await BuildToken(new UserLogin { Email = dTO.Email, Password = dTO.Password }, new List<string>());
                return new ApiResponse<UsuarioToken> { Result = token };
            }
            else
            {
                return new ApiResponse<UsuarioToken> { Result = null, StatusResponse = StatusResponse.BadRequest ,MessageError = "Usuario o contraseña incorrecta.", StatusCode = 400 };
            }
        }

        private async Task<UsuarioToken> BuildToken(UserLogin userInfo, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

          
            var identityUser = await userManager.FindByEmailAsync(userInfo.Email);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, identityUser.Id));
            var claimsDB = await userManager.GetClaimsAsync(identityUser);

            claims.AddRange(claimsDB);

            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddYears(1);

            JwtSecurityToken token = new(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);
            return new UsuarioToken { Expiration = DateTime.Now.AddYears(1), Token = new JwtSecurityTokenHandler().WriteToken(token) };
        }
    }
}
