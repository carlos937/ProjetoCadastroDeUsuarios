using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IUsuarioService
    {
        Task<List<UsuarioModel>> buscarTodos();
        Task<ServerStatus> adicionar(UsuarioModel usuarioModel);
        Task<ServerStatus> atualizar(UsuarioModel usuarioModel);
        Task<ServerStatus> alterarSenha(UsuarioModel usuarioModel);
        Task<ServerStatus> remover(Guid id);
        Task<ServerStatus> login(LoginModel loginModel);
 
    }
}
