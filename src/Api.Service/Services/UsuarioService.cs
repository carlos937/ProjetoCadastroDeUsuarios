using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Domain.Interfaces.Repositorio;
using Domain.Interfaces.Service;
using Domain.Models;
using Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IRepositorioUsuario _repositorio;

        private TokenConfigurations _tokenConfigurations;

        private SigningConfigurations _signingConfigurations;

        public IConfiguration _configuration { get; }


        public UsuarioService(
            IRepositorioUsuario repositorio,
            IConfiguration configuration ,
            TokenConfigurations tokenConfigurations,
            SigningConfigurations signingConfigurations
            )
        {
            _configuration = configuration;
            _repositorio = repositorio;
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
        }

        public async Task<List<UsuarioModel>> buscarTodos()
        {
            var usuarios = new List<UsuarioModel>();

            _repositorio.getUsuarios().Result.ToList().ForEach(u =>
            {
                usuarios.Add(new UsuarioModel()
                {
                    id = u.id,
                    email = u.email,
                    nome = u.nome
                });
            });

            return await Task.FromResult(usuarios);
        }

        public async Task<ServerStatus> adicionar(UsuarioModel usuarioModel)
        {
            try
            {
                usuarioModel.email = usuarioModel.email.Trim();
                var usuario = new Usuario(usuarioModel.nome, usuarioModel.email, usuarioModel.senha);

                await _repositorio.InsertAsync(usuario);
                usuarioModel.id = usuario.id;
                usuarioModel.status = 0;
                usuarioModel.mensagem = "Usuario adicionado com sucesso.";
                return usuarioModel;
            }
            catch (Exception ex)
            {
                return new ServerStatus()
                {
                    status = -1,
                    mensagem = ex.Message
                };
            }
        }

        public async Task<ServerStatus> atualizar(UsuarioModel usuarioModel)
        {
            try
            {
                usuarioModel.email = usuarioModel.email.Trim();
                var usuario = _repositorio.find(usuarioModel.id).Result;
                usuario.setEmail(usuarioModel.email);
                usuario.setNome(usuarioModel.nome);
                usuario.setSenha(usuarioModel.senha);
                await _repositorio.updateAsync(usuario);
                usuarioModel.id = usuario.id;
                usuarioModel.status = 0;
                usuarioModel.mensagem = "Usuario atualizado com sucesso.";
                return usuarioModel;
            }
            catch (Exception ex)
            {
                return new ServerStatus()
                {
                    status = -1,
                    mensagem = ex.Message
                };
            }
        }
        public async Task<ServerStatus> remover(Guid id)
        {
            try
            {
                var usuario = _repositorio.find(id).Result;
                usuario.lixeira = true;
                await _repositorio.updateAsync(usuario);
                return new ServerStatus()
                {
                    status = 0,
                    mensagem = "Removido com sucesso"
                };
            }
            catch (Exception ex)
            {
                return new ServerStatus()
                {
                    status = -1,
                    mensagem = ex.Message
                };
            }
        }

        public async Task<ServerStatus> login(UsuarioModel usuarioModel)
        {
            try
            {
                usuarioModel.email = usuarioModel.email.Trim();
                var usuario = await _repositorio.getUsuarioLogin(usuarioModel.email, usuarioModel.senha);

                if (usuario != null)
                {

                    var jwtConfiguracoes = new JWTConfiguracoes(_tokenConfigurations,_signingConfigurations);

                    var token = jwtConfiguracoes.gerarToken(usuarioModel.email);

                    return new UsuarioModel()
                    {
                        id = usuario.id,
                        email = usuario.email,
                        status = 0,
                        dataDeAtualizacao =  usuario.dataDeAtualizacao,
                        dataDeCadastro = usuario.dataDeCadastro,
                        mensagem = "Login efetuado com sucesso.",
                        created = jwtConfiguracoes.createdDateToken.ToString(),
                        expiration = jwtConfiguracoes.expirationDateToken.ToString(),
                        token = token,
                        nome = usuario.nome
                    };

                }
                else
                {
                    return new ServerStatus()
                    {
                        status = 1,
                        mensagem = "Usuario não encontrado, verifique se sua senha e email estão corretos."
                    };
                }

            }
                
            catch (Exception ex)
            {
                return new ServerStatus() { mensagem = ex.Message, status = -1 };
            }
        }

    }
}
