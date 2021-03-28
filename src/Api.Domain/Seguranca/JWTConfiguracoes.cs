using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces.Seguranca;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
namespace Domain.Security
{
    public class JWTConfiguracoes : IJWTConfiguracoes
    {
        private TokenConfigurations _tokenConfigurations;

        private SigningConfigurations _signingConfigurations;

        public DateTime createdDateToken { get; private set; }
        public DateTime expirationDateToken { get; private set; }

        public JWTConfiguracoes(
            TokenConfigurations tokenConfigurations,
            SigningConfigurations signingConfigurations)
        {
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;

        }


        public DateTime getCreatedDateToken()
        {
            return createdDateToken;
        }
        public DateTime getExpirationDateToken()
        {
            return createdDateToken;
        }


        public string gerarToken(string identificacao)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                       new GenericIdentity(identificacao),
                       new[]
                       {
                            new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, identificacao)
                       }
                   );

            this.createdDateToken = HelperHorario.HoraDeBrasilia;
            this.expirationDateToken = this.createdDateToken + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();


           return CreateToken(identity, this.createdDateToken, this.expirationDateToken, handler);
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            return handler.WriteToken(securityToken);
        }

    }
}
