using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _serviceUsuario;
        public UsuarioController(IUsuarioService serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }
        [Authorize("Bearer")]
        [HttpGet("SelectAll")]
        public async Task<List<UsuarioModel>> SelectAll()
        {
            return await _serviceUsuario.buscarTodos();
        }
      
        [HttpPost("Adicionar")]
        public async Task<ServerStatus> Adicionar(UsuarioModel usuarioModel)
        {
            return await _serviceUsuario.adicionar(usuarioModel);
        }
        [Authorize("Bearer")]
        [HttpPost("Atualizar")]
        public async Task<ServerStatus> Atualizar(UsuarioModel usuarioModel)
        {
            return await _serviceUsuario.atualizar(usuarioModel);
        }
        [Authorize("Bearer")]
        [HttpGet("Remover")]
        public async Task<ServerStatus> Remover(Guid id)
        {
            return await _serviceUsuario.remover(id);
        }

        [HttpPost("Login")]
        public async Task<ServerStatus> login(UsuarioModel usuarioModel)
        {
            return await _serviceUsuario.login(usuarioModel);
        }
   
    }
   }
