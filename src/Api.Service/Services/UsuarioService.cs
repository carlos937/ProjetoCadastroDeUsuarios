using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositorio;
using Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IRepositorioUsuario _repositorio;

        public UsuarioService(IRepositorioUsuario repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Usuario>> buscarTodos()
        {
            return await Task.FromResult(_repositorio.selectAsync().Result.ToList());
        }
    }
}
