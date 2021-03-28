using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Seguranca
{
    public interface IJWTConfiguracoes
    {
        string gerarToken(string identificacao);
        DateTime getCreatedDateToken();
        DateTime getExpirationDateToken();

    }
}
