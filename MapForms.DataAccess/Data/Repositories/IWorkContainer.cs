using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapForms.DataAccess.Data.Repositories
{
    public interface IWorkContainer
    {
        IUsuarioRepository Usuarios { get; }
    }
}
