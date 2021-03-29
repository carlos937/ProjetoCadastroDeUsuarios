using Domain.Entities;
using Domain.Helpers.Extensoes;
using Domain.Interfaces.Service;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace cadastro_de_usuarios_teste
{
    [Collection(nameof(UsuarioServiceColletion))]
    public class UsuarioServiceTeste
    {
        private readonly UsuarioFixture _usuarioFixture;
        private readonly IUsuarioService _usuarioService;

        public UsuarioServiceTeste(UsuarioFixture usuarioFixture)
        {
            _usuarioFixture = usuarioFixture;

           _usuarioService = usuarioFixture.GerarService();

        }

        [Fact(DisplayName = "UsuarioService_BuscarTodos_RetornarListasDeUsuarios")]
        public async Task UsuarioService_BuscarTodos_RetornarListasDeUsuarios()
        {

            //arrange
            _usuarioFixture._iUsuarioServiceMock.Setup(s => s.buscarTodos()).ReturnsAsync(_usuarioFixture.GerarListaUsuarioModel());
            _usuarioFixture._iUsuarioRepositorioMock.Setup(s => s.getUsuarios()).ReturnsAsync(_usuarioFixture.GerarListaUsuario());
            _usuarioFixture._mapperMock.Setup(s => s.Map<UsuarioModel>(It.IsAny<Usuario>())).Returns(_usuarioFixture.GerarUsuarioModel());

            //act
            var resultado = await _usuarioService.buscarTodos();

            //assert
            Assert.True(resultado.Any());

        }

        [Fact(DisplayName = "UsuarioService_Adicionar_RetornarUsuarioAdicionado")]
        public async Task UsuarioService_Adicionar_RetornarUsuarioAdicionado()
        {

            //arrange
            _usuarioFixture._iUsuarioServiceMock.Setup(s => s.adicionar(It.IsAny<UsuarioModel>())).ReturnsAsync(_usuarioFixture.GerarUsuarioModel());
            _usuarioFixture._mapperMock.Setup(s => s.Map<Usuario>(It.IsAny<UsuarioModel>())).Returns(_usuarioFixture.GerarUsuario());
            //act
            var usuarioModel = _usuarioFixture.GerarUsuarioModel();
            var resultado = await _usuarioService.adicionar(usuarioModel);

            //assert
            Assert.NotNull(resultado);
            Assert.True(resultado.status == 0);

        }

        [Fact(DisplayName = "UsuarioService_Atualizar_RetornarUsuarioAtualizado")]
        public async Task UsuarioService_Atualizar_RetornarUsuarioAtualizado()
        {

            //arrange
            _usuarioFixture._iUsuarioServiceMock.Setup(s => s.atualizar(It.IsAny<UsuarioModel>())).ReturnsAsync(_usuarioFixture.GerarUsuarioModel());
            _usuarioFixture._iUsuarioRepositorioMock.Setup(s => s.find(It.IsAny<Guid>())).ReturnsAsync(_usuarioFixture.GerarUsuario());
            //act
            var resultado = await _usuarioService.atualizar(_usuarioFixture.GerarUsuarioModel());

            //assert
            Assert.NotNull(resultado);
            Assert.True(resultado.status == 0);

        }

        [Fact(DisplayName = "UsuarioService_AlterarSenha_RetornarUsuarioAtualizado")]
        public async Task UsuarioService_AlterarSenha_RetornarUsuarioAtualizado()
        {

            //arrange

            _usuarioFixture._iUsuarioServiceMock.Setup(s => s.alterarSenha(It.IsAny<UsuarioModel>())).ReturnsAsync(_usuarioFixture.GerarUsuarioModel());
            _usuarioFixture._mapperMock.Setup(s => s.Map<UsuarioModel>(It.IsAny<Usuario>())).Returns(_usuarioFixture.GerarUsuarioModel());
            _usuarioFixture._iUsuarioRepositorioMock.Setup(s => s.find(It.IsAny<Guid>())).ReturnsAsync(_usuarioFixture.GerarUsuario());

            //act
            var resultado = await _usuarioService.alterarSenha(_usuarioFixture.GerarUsuarioModel());

            //assert
            Assert.NotNull(resultado);
            Assert.True(resultado.status == 0);

        }

        [Fact(DisplayName = "UsuarioService_Remover_RetornarServerStatus")]
        public async Task UsuarioService_Remover_RetornarServerStatus()
        {

            //arrange

            _usuarioFixture._iUsuarioServiceMock.Setup(s => s.remover(It.IsAny<Guid>())).ReturnsAsync(_usuarioFixture.GerarServerStatus());
            _usuarioFixture._iUsuarioRepositorioMock.Setup(s => s.find(It.IsAny<Guid>())).ReturnsAsync(_usuarioFixture.GerarUsuario());

            //act

            var resultado = await _usuarioService.remover(new Guid());

            //assert

            Assert.NotNull(resultado);
            Assert.True(resultado.status == 0);

        }

        [Fact(DisplayName = "UsuarioService_Login_RetornarLoginModel")]
        public async Task UsuarioService_Login_RetornarLoginModel()
        {

            //arrange

            _usuarioFixture._iUsuarioServiceMock.Setup(s => s.login(It.IsAny<LoginModel>())).ReturnsAsync(_usuarioFixture.GerarLoginModel());
            _usuarioFixture._iUsuarioRepositorioMock.Setup(s => s.getUsuarioLogin(It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(_usuarioFixture.GerarUsuario());
            _usuarioFixture._jwtConfiguracoes.Setup(s => s.gerarToken(It.IsAny<string>())).Returns(StringExtension.alfanumericoAleatorio(20));
           
            //act

            var resultado = await _usuarioService.login(_usuarioFixture.GerarLoginModel());

            //assert

            Assert.NotNull(resultado);
            Assert.True(resultado.status == 0);

        }


    }
}
