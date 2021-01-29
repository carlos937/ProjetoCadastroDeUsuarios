using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorios
{
    public class RepositorioUsuario : Repositorio<Usuario>,IRepositorioUsuario
    {
        public RepositorioUsuario(MyContext context) : base(context)
        {

        }

        public async Task<List<Usuario>> getUsuarios()
        {
            return await  Task.FromResult(_context.usuarios.Where(u => !u.lixeira).ToList());
        }

        public async Task<Usuario> getUsuarioLogin(string email )
        {
            return await Task.FromResult(_context.usuarios.FirstOrDefault(u => u.email == email));
        }

    }
}
