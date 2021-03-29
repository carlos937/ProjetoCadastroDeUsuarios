using AutoMapper;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Domain.Interfaces.Repositorio;
using Domain.Interfaces.Seguranca;
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

        public readonly IMapper _mapper;

        public IJWTConfiguracoes _jwtConfiguracoes;

        public UsuarioService(
            IRepositorioUsuario repositorio,
            IConfiguration configuration ,
            TokenConfigurations tokenConfigurations,
            SigningConfigurations signingConfigurations,
            IMapper mapper,
            IJWTConfiguracoes jWTConfiguracoes
            )
        {
            _configuration = configuration;
            _repositorio = repositorio;
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
            _mapper = mapper;
            _jwtConfiguracoes = jWTConfiguracoes;
        }

        public async Task<List<UsuarioModel>> buscarTodos()
        {
            var usuarios = new List<UsuarioModel>();

            _repositorio.getUsuarios().Result.ToList().ForEach(u =>
            {
                usuarios.Add(_mapper.Map<UsuarioModel>(u));
            });

            return await Task.FromResult(usuarios);
        }

        public async Task<ServerStatus> adicionar(UsuarioModel usuarioModel)
        {
            try
            {
                usuarioModel.email = usuarioModel.email.Trim();

                if((await _repositorio.getUsuarioEmail(usuarioModel.email)) != null)
                {
                    return new ServerStatus()
                    {
                        status = 1,
                        mensagem = "Este email já se encontra cadastrado em nosso sistema."
                    };
                }

                var usuario = _mapper.Map<Usuario>(usuarioModel);
                await _repositorio.InsertAsync(usuario);

                var token = _jwtConfiguracoes.gerarToken(usuarioModel.email);

                var jsonWebTokenModel = new JsonWebTokenModel()
                {
                    created = _jwtConfiguracoes.getCreatedDateToken(),
                    expiration = _jwtConfiguracoes.getExpirationDateToken(),
                    token = token
                };

                return new LoginModel()
                {
                    id = usuario.id,
                    email = usuario.email,
                    status = 0,
                    mensagem = "Login efetuado com sucesso.",
                    dataDeAtualizacao = usuario.dataDeAtualizacao,
                    dataDeCadastro = usuario.dataDeCadastro,
                    nome = usuario.nome,
                    jsonWebToken = jsonWebTokenModel
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

        public async Task<ServerStatus> atualizar(UsuarioModel usuarioModel)
        {
            try
            {
                if ((await _repositorio.getUsuarioEmail(usuarioModel.email)) != null)
                {
                    return new ServerStatus()
                    {
                        status = 1,
                        mensagem = "Este email já se encontra cadastrado em nosso sistema."
                    };
                }
                var usuario = _repositorio.find(usuarioModel.id).Result;
                usuario.setEmail(usuarioModel.email.Trim());
                usuario.setNome(usuarioModel.nome);
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
        public async Task<ServerStatus> alterarSenha(UsuarioModel usuarioModel)
        {
            try
            {
                var usuario = _repositorio.find(usuarioModel.id).Result;
                usuario.setSenha(usuarioModel.senha);
                await _repositorio.updateAsync(usuario);
                usuarioModel.id = usuario.id;
                usuarioModel.status = 0;
                usuarioModel.mensagem = "Senha alterada com sucesso.";
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

        public async Task<ServerStatus> login(LoginModel usuarioModel)
        {
            try
            {
                usuarioModel.email = usuarioModel.email.Trim();
                var usuario = await _repositorio.getUsuarioLogin(usuarioModel.email, usuarioModel.senha);

                if (usuario != null)
                {
                   

                    var token = _jwtConfiguracoes.gerarToken(usuarioModel.email);

                    var jsonWebTokenModel = new JsonWebTokenModel()
                    {
                        created = _jwtConfiguracoes.getCreatedDateToken(),
                        expiration = _jwtConfiguracoes.getExpirationDateToken(),
                        token = token
                    };

                    return new LoginModel()
                    {
                        id = usuario.id,
                        email = usuario.email,
                        status = 0,
                        mensagem = "Login efetuado com sucesso.",
                        dataDeAtualizacao =  usuario.dataDeAtualizacao,
                        dataDeCadastro = usuario.dataDeCadastro,
                        nome = usuario.nome,
                        jsonWebToken = jsonWebTokenModel
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
