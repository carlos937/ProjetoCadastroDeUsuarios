using Domain.Interfaces.Seguranca;
using Domain.Security;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.JWT
{
    public class ConfigureJWT
    {
            public static void ConfigureDependenciesJWT(IServiceCollection serviceCollection)
            {
                serviceCollection.AddTransient<IJWTConfiguracoes, JWTConfiguracoes>();
            }
    }
}
