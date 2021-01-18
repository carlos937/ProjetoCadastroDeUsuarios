using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> buscarTodos();
    }
}
