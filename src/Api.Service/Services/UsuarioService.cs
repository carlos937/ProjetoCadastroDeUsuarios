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
using System.Threading.Tasks;

namespace Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IRepositorioUsuario _repositorio;

        private SigningConfigurations _signingConfigurations;

        private TokenConfigurations _tokenConfigurations;

        public IConfiguration _configuration { get; }


        public UsuarioService(
            IRepositorioUsuario repositorio,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations,
            IConfiguration configuration
            )
        {
            _configuration = configuration;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _repositorio = repositorio;
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
                var usuario = new Usuario()
                {
                    email = usuarioModel.email,
                    nome = usuarioModel.nome
                };
                await _repositorio.InsertAsync(usuario);
                usuarioModel.id = usuario.id;
                usuarioModel.status = 0;
                usuarioModel.mensagem = "Usuario adicionado com sucesso.";
                return usuarioModel;
            }
            catch(Exception ex)
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
                var usuario = _repositorio.find(usuarioModel.id).Result;
                usuario.email = usuarioModel.email;
                usuario.nome = usuarioModel.nome;
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
                var usuario = await _repositorio.getUsuarioLogin(usuarioModel.email);

                if(usuario != null)
                {


                    ClaimsIdentity identity = new ClaimsIdentity(

                        new GenericIdentity(usuario.email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.email)
                        }
                    ); ;

                    var createDate = HelperHorario.HoraDeBrasilia;
                    var expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();

                    return new UsuarioModel()
                    {
                        id = usuario.id,
                        email = usuario.email,
                        status = 0,
                        mensagem = "Login efetuado com sucesso.",
                        created = createDate.ToString(),
                        expiration = expirationDate.ToString(),
                        token = CreateToken(identity, createDate, expirationDate, handler),
                        nome = usuario.nome
                    };

                }
                else
                {
                    return new UsuarioModel()
                    {
                        id = usuario.id,
                        email = usuario.email,
                        status = 1,
                        mensagem = "Usuario não encontrado, verifique se sua senha esta correta."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServerStatus() { mensagem = ex.Message , status = -1 };
            }
        }

        private string CreateToken(ClaimsIdentity identity , DateTime createDate ,DateTime expirationDate , JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            return handler.WriteToken(securityToken);
        }


    }
}
