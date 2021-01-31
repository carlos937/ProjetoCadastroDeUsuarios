using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositorio
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {

        Task<List<Usuario>> getUsuarios();
        Task<Usuario> getUsuarioLogin(string email, string senha);
    }
}
