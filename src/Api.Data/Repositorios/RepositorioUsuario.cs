using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositorios
{
    public class RepositorioUsuario : Repositorio<Usuario>,IRepositorioUsuario
    {
        public RepositorioUsuario(MyContext context) : base(context)
        {

        }



    }
}
