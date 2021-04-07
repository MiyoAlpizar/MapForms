using MapForms.Models.DTOS;
using MapForms.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapForms.DataAccess.Data.Repositories
{
    public interface IUsuarioRepository
    {
        public Task<IEnumerable<UsuarioDTO>> GetUsuariosList();

        public Task<ApiResponse<UsuarioToken>> RegisterUser(UsuarioCreateDTO dTO);

        public Task<ApiResponse<UsuarioToken>> LoginUser(UserLogin userLogin);

    }
}
