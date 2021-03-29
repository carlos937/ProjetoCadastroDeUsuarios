using AutoMapper;
using AutoMoqCore;
using Bogus;
using Domain.Entities;
using Domain.Helpers;
using Domain.Helpers.Extensoes;
using Domain.Interfaces.Repositorio;
using Domain.Interfaces.Seguranca;
using Domain.Interfaces.Service;
using Domain.Models;
using Domain.Security;
using Moq;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace cadastro_de_usuarios_teste
{
    [CollectionDefinition(nameof(UsuarioColletion))]
    public class UsuarioColletion : ICollectionFixture<UsuarioFixture> {  }

    [CollectionDefinition(nameof(UsuarioServiceColletion))]
    public class UsuarioServiceColletion : ICollectionFixture<UsuarioFixture> { }
    public class UsuarioFixture
    {
        public Mock<IMapper> _mapperMock { get; set; }
        public Mock<IUsuarioService>  _iUsuarioServiceMock { get; set; }
        public Mock<IRepositorioUsuario>  _iUsuarioRepositorioMock { get; set; }
        public Mock<IJWTConfiguracoes> _jwtConfiguracoes { get; set; }

        public string senhaRsa = CryptografiaRSA.encrypt(StringExtension.alfanumericoAleatorio(10));
        public IUsuarioService GerarService()
        {

            var mocker = new AutoMoqer();
            mocker.Create<UsuarioService>();
            var service = mocker.Resolve<UsuarioService>();

            _iUsuarioServiceMock = mocker.GetMock<IUsuarioService>();
            _iUsuarioRepositorioMock = mocker.GetMock<IRepositorioUsuario>();
            _mapperMock = mocker.GetMock<IMapper>();
            _jwtConfiguracoes = mocker.GetMock<IJWTConfiguracoes>();

            return service;
        }
        public List<UsuarioModel> GerarListaUsuarioModel()
        {

            return new Faker<List<UsuarioModel>>("pt_BR")
                .CustomInstantiator(f => new List<UsuarioModel>() { GerarUsuarioModel() });

        }
        public UsuarioModel GerarUsuarioModel()
        {


            return new Faker<UsuarioModel>("pt_BR")
                .CustomInstantiator(f => 
                 new UsuarioModel()
                 {
                     id = f.Random.Guid(),
                     email = f.Person.Email,
                     nome = f.Person.UserName,
                     dataDeAtualizacao = f.Date.Recent(),
                     dataDeCadastro = f.Date.Recent(),
                     senha = senhaRsa,
                 });

        }

        public ServerStatus GerarServerStatus()
        {

            return new Faker<ServerStatus>("pt_BR")
                .CustomInstantiator(f =>
                 new ServerStatus()
                 {
                     status = 0,
                 });

        }

        public List<Usuario> GerarListaUsuario()
        {

            return new Faker<List<Usuario>>("pt_BR")
                .CustomInstantiator(f => new List<Usuario>() { GerarUsuario()});

        }

        public Usuario GerarUsuario()
        {

            return new Faker<Usuario>("pt_BR")
                .CustomInstantiator(f => 
                 new Usuario(f.Person.UserName, f.Person.Email, senhaRsa)
                 {
                       id = f.Random.Guid(),
                       dataDeAtualizacao = f.Date.Recent(),
                       dataDeCadastro = f.Date.Recent()
                 });

        }

        public LoginModel GerarLoginModel()
        {

            return new Faker<LoginModel>("pt_BR")
                .CustomInstantiator(f =>
                 new LoginModel()
                 {
                     id = f.Random.Guid(),
                     email = f.Person.Email,
                     status = 0,
                     nome = f.Person.UserName,
                     dataDeAtualizacao = f.Date.Recent(),
                     dataDeCadastro = f.Date.Recent(),
                     senha = senhaRsa,
                     jsonWebToken = new JsonWebTokenModel()
                     {
                          created = f.Date.Recent(),
                          expiration = f.Date.Future(),
                          token = StringExtension.alfanumericoAleatorio(20)
                     }
                 });

        }

    }
}
