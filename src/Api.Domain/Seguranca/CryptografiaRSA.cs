using Domain.Models;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public static class CryptografiaRSA
    {

        private static RSACryptoServiceProvider _chavePrivada;
        private static RSACryptoServiceProvider _chavePublica;


        public static string encrypt(string text)
        {
            // busca chave publica no arquivo e devolve um objeto RSACryptoServiceProvider
            _chavePublica = BuscarChavePublicaNoArquivo();
            // passa o resultado para bytes e chama o metodo para encriptar a mensagem
            var encryptedBytes = _chavePublica.Encrypt(Encoding.UTF8.GetBytes(text), false);
            // devolve o resultado convertido para base 64
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string decrypt(string encrypted)
        {
            // busca chave privada no arquivo e devolve um objeto RSACryptoServiceProvider
            _chavePrivada = BuscarChavePrivadaNoArquivo();
            // passa o resultado para bytes e chama o metodo para descritar a mensagem
            var decryptedBytes = _chavePrivada.Decrypt(Convert.FromBase64String(encrypted), false);
            // devolve o resultado convertido em string
            return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
        }

        private static RSACryptoServiceProvider BuscarChavePrivadaNoArquivo()
        {
            // busca arquivo
            using (TextReader privateKeyTextReader = new StringReader(File.ReadAllText(@".\keys\chaveprivada.pem")))
            {
                // transforma o conteudo da chave pem em um objeto RSAParameters
                AsymmetricCipherKeyPair readKeyPair = (AsymmetricCipherKeyPair)new PemReader(privateKeyTextReader).ReadObject();
                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)readKeyPair.Private);
                
                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                // importa parametros da chave para a instacia de RSACryptoServiceProvider
                csp.ImportParameters(rsaParams);
                // retorna a instancia de RSACryptoServiceProvider
                return csp;
            }
        }

        private static RSACryptoServiceProvider BuscarChavePublicaNoArquivo()
        {
            // busca arquivo
            using (TextReader publicKeyTextReader = new StringReader(File.ReadAllText(@".\keys\chavepublica.pem")))
            {
                // transforma o conteudo da chave pem em um objeto RSAParameters
                RsaKeyParameters publicKeyParam = (RsaKeyParameters)new PemReader(publicKeyTextReader).ReadObject();
                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters(publicKeyParam);

                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                // importa parametros da chave para a instacia de RSACryptoServiceProvider
                csp.ImportParameters(rsaParams);
                // retorna a instancia de RSACryptoServiceProvider
                return csp;
            }
        }

    }

}
