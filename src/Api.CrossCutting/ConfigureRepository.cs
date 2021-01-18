using Data.Repositorios;
using Domain.Interfaces;
using Domain.Interfaces.Repositorio;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
            serviceCollection.AddScoped(typeof(IRepositorioUsuario), typeof(RepositorioUsuario));
        }
}
}
