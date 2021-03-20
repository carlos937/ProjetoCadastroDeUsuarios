using AutoMapper;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Mappings
{
    public class EntityToModelProfile:Profile
    {

        public EntityToModelProfile()
        {
            CreateMap<UsuarioModel, Usuario>().ConstructUsing(u => new Usuario(u.nome,u.email,u.senha));
            CreateMap<Usuario, UsuarioModel>();
        }

    }
}
