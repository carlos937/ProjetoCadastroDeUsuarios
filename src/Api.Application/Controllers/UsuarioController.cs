﻿using System;
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
        public UsuarioController()
        {
        }

        [Authorize("Bearer")]
        [HttpGet("BuscarTodos")]
        public async Task<List<UsuarioModel>> BuscarTodos([FromServices] IUsuarioService _serviceUsuario)
        {
            return await _serviceUsuario.buscarTodos();
        }
      
        [HttpPost("Adicionar")]
        public async Task<ServerStatus> Adicionar(UsuarioModel usuarioModel, [FromServices] IUsuarioService _serviceUsuario)
        {
            return await _serviceUsuario.adicionar(usuarioModel);
        }
        [Authorize("Bearer")]
        [HttpPost("Atualizar")]
        public async Task<ServerStatus> Atualizar(UsuarioModel usuarioModel, [FromServices] IUsuarioService _serviceUsuario)
        {
            return await _serviceUsuario.atualizar(usuarioModel);
        }

        [Authorize("Bearer")]
        [HttpPost("AlterarSenha")]
        public async Task<ServerStatus> AlterarSenha(UsuarioModel usuarioModel, [FromServices] IUsuarioService _serviceUsuario)
        {
            return await _serviceUsuario.alterarSenha(usuarioModel);
        }

        [Authorize("Bearer")]
        [HttpGet("Remover")]
        public async Task<ServerStatus> Remover(Guid id, [FromServices] IUsuarioService _serviceUsuario)
        {
            return await _serviceUsuario.remover(id);
        }

        [HttpPost("Login")]
        public async Task<ServerStatus> login(LoginModel loginModel, [FromServices] IUsuarioService _serviceUsuario)
        {
            return await _serviceUsuario.login(loginModel);
        }
   
    }
   }
