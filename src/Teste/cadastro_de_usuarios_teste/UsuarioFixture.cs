using Bogus;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace cadastro_de_usuarios_teste
{
    [CollectionDefinition(nameof(UsuarioColletion))]
    public class UsuarioColletion : ICollectionFixture<UsuarioFixture> {  }
    public class UsuarioFixture
    {

        public List<UsuarioModel> GerarListaUsuarioModel()
        {

            return new Faker<List<UsuarioModel>>("pt_BR")
                .CustomInstantiator(f => new List<UsuarioModel>() { 
                 new UsuarioModel()
                 {
                     id = f.Random.Guid(),
                     email = f.Person.Email,
                     nome = f.Person.UserName,
                     dataDeAtualizacao = f.Date.Recent(),
                     senha = Convert.ToBase64String(f.Random.Bytes(256)),
                 }
                });

        }



    }
}
