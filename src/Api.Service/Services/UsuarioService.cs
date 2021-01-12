using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IRepositorio<Usuario> _repositorio;

        public UsuarioService(IRepositorio<Usuario> repositorio)
        {
            _repositorio = repositorio;
        }


    }
}
