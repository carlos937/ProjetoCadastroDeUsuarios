using Application.Controllers;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace cadastro_de_usuarios_teste
{
    [Collection(nameof(UsuarioColletion))]
    public class UsuarioControllerTeste
    {

        private readonly UsuarioFixture _usuarioFixture;
        private readonly UsuarioController _usuarioController; 
        public UsuarioControllerTeste(UsuarioFixture usuarioFixture)
        {
            _usuarioFixture = usuarioFixture;

            usuarioFixture.GerarService();

            _usuarioController = new UsuarioController();
        }

        [Fact(DisplayName = "UsuarioController_BuscarTodos_RetornarListasDeUsuarios")]
        public async Task UsuarioController_BuscarTodos_RetornarListasDeUsuarios()
        {

            //arrange
            _usuarioFixture._iUsuarioServiceMock.Setup(s => s.buscarTodos()).ReturnsAsync(_usuarioFixture.GerarListaUsuarioModel());
            //act
            var resultado =  await _usuarioController.BuscarTodos(_usuarioFixture._iUsuarioServiceMock.Object);

            //assert
            Assert.True(resultado.Any());

        }

        [Fact(DisplayName = "UsuarioController_Adicionar_RetornarUsuarioAdicionado")]
        public async Task UsuarioController_Adicionar_RetornarUsuarioAdicionado()
        {

            //arrange
            _usuarioFixture._iUsuarioServiceMock.Setup(s => s.adicionar(It.IsAny<UsuarioModel>())).ReturnsAsync(_usuarioFixture.GerarUsuarioModel());
            //act
            var resultado = await _usuarioController.Adicionar(_usuarioFixture.GerarUsuarioModel(),_usuarioFixture._iUsuarioServiceMock.Object);
            //assert
            Assert.NotNull(resultado);
            Assert.True(resultado.status == 0);

        }

        [Fact(DisplayName = "UsuarioController_Atualizar_RetornarUsuarioAtualizado")]
        public async Task UsuarioController_Atualizar_RetornarUsuarioAtualizado()
        {

            //arrange
            _usuarioFixture._iUsuarioServiceMock.Setup(s => s.atualizar(It.IsAny<UsuarioModel>())).ReturnsAsync(_usuarioFixture.GerarUsuarioModel());
         
            //act
            var resultado = await _usuarioController.Atualizar(_usuarioFixture.GerarUsuarioModel(), _usuarioFixture._iUsuarioServiceMock.Object);

            //assert
            Assert.NotNull(resultado);
            Assert.True(resultado.status == 0);

        }

        [Fact(DisplayName = "UsuarioController_AlterarSenha_RetornarUsuarioAtualizado")]
        public async Task UsuarioController_AlterarSenha_RetornarUsuarioAtualizado()
        {

            //arrange

            _usuarioFixture._iUsuarioServiceMock.Setup(s => s.alterarSenha(It.IsAny<UsuarioModel>())).ReturnsAsync(_usuarioFixture.GerarUsuarioModel());

            //act
            var resultado = await _usuarioController.AlterarSenha(_usuarioFixture.GerarUsuarioModel(), _usuarioFixture._iUsuarioServiceMock.Object);

            //assert
            Assert.NotNull(resultado);
            Assert.True(resultado.status == 0);

        }

        [Fact(DisplayName = "UsuarioController_Remover_RetornarServerStatus")]
        public async Task UsuarioController_Remover_RetornarServerStatus()
        {

            //arrange

            _usuarioFixture._iUsuarioServiceMock.Setup(s => s.remover(It.IsAny<Guid>())).ReturnsAsync(_usuarioFixture.GerarServerStatus());

            //act

            var resultado = await _usuarioController.Remover(new Guid(), _usuarioFixture._iUsuarioServiceMock.Object);

            //assert

            Assert.NotNull(resultado);
            Assert.True(resultado.status == 0);

        }

        [Fact(DisplayName = "UsuarioController_Login_RetornarLoginModel")]
        public async Task UsuarioController_Login_RetornarLoginModel()
        {

            //arrange

            _usuarioFixture._iUsuarioServiceMock.Setup(s => s.login(It.IsAny<LoginModel>())).ReturnsAsync(_usuarioFixture.GerarLoginModel());

            //act

            var resultado = await _usuarioController.login(_usuarioFixture.GerarLoginModel(), _usuarioFixture._iUsuarioServiceMock.Object);

            //assert

            Assert.NotNull(resultado);
            Assert.True(resultado.status == 0);

        }

    }
}
