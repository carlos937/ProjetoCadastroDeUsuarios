using Domain.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public static class CryptografiaRSA
    {


        private static string chavePublica = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<RSAParameters xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Exponent>AQAB</Exponent>\r\n  <Modulus>32Dak8o063bUEXmkSC5t3Pp3XzCESWMU6QFyns5VWPXN7pzU3C6/BK7dwGRucrUaJU77mjPNgsz/2MlQ789YVJbOoHq1wecZoS/E8a9V5CLg+sdV7wZak640Rm77WLs/UD6qv0CE1RV6M2LlMHZ8xb9TJRA8pVLCrZINYfYvD9U=</Modulus>\r\n</RSAParameters>";

        private static string chavePrivada = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<RSAParameters xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <D>mq2RjYcWj/oPrS4jMQxdbQhPAq4w3DRs9U/YU9kixmAnQlR9HR6NjSudSC0DQhDf0vfXR5cZdrqHa1Ez52sXiOgPC7N43iBdfHm5k6nUU61G28fa9lcto7WzKgOhAJoNBBTrTZKfrm766R4Jxla0v/b6714uLF7uFCOIWXBSBD0=</D>\r\n  <DP>jey3+9vdAmWEFhvgNB4Mm6cBow0Iz2QAJMGqwDowErKKYMVP0r/oUsR+snkGuPdrluJJX4qMitZMbf5PG+j2FQ==</DP>\r\n  <DQ>qv253T5zot5WabHTXFst6+IeMC3U1X4tYUhDZ1sOCXmkwxUhKB6IZOgKLpc1dG26C30+BJHPvEKSXlcC+1HroQ==</DQ>\r\n  <Exponent>AQAB</Exponent>\r\n  <InverseQ>XfHSU5o6HZYr2BSIvgsOItxJeeqletfDXqjwh2BCvpjPLNtyekbKkF87PgUcCjMHdWPnB8zkoFwSlKWrrnHlJg==</InverseQ>\r\n  <Modulus>32Dak8o063bUEXmkSC5t3Pp3XzCESWMU6QFyns5VWPXN7pzU3C6/BK7dwGRucrUaJU77mjPNgsz/2MlQ789YVJbOoHq1wecZoS/E8a9V5CLg+sdV7wZak640Rm77WLs/UD6qv0CE1RV6M2LlMHZ8xb9TJRA8pVLCrZINYfYvD9U=</Modulus>\r\n  <P>+W6VJEKuD1AT9g8efqm87QIqHGmVnwPo8DyE+fqXpIUNoR89hopk73z4BAHgIdGKbiEXczWzELqry2eTG2tz4w==</P>\r\n  <Q>5UKk4uHWcY4rHR7n219/qO9gi9YgssFARP6ZmRbSL832w+Kxd76Oo/48SOklA/SDBedp2Fz0FzlPXE9HaETq5w==</Q>\r\n</RSAParameters>";


        public static async Task<string> encrypt(string texto)
        {
            return await criptografia(texto, true);
        }

        public static async Task<string> decrypt(string texto)
        {
            return await criptografia(texto, false);
        }
        private static async Task<string> criptografia(string texto, bool toogle)
        {
            try
            {
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                byte[] resultado;
                using (RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider())
                {
                    // verifica se vai encriptar ou descriptar a mensagem
                    if (toogle)
                    {
                        // converte o texto em bytes
                        byte[] dataToEncrypt = ByteConverter.GetBytes(texto);
                        // chama função para encriptar mensagem 
                        resultado = await RSAEncrypt(dataToEncrypt, false, objRSA);
                        // devolve a mensagem encriptada em base 64
                        return Convert.ToBase64String(resultado);
                    }
                    else
                    {
                        // transforma a mensagem de base 64 para bytes
                        var dataToEncrypt = Convert.FromBase64String(texto);
                        // chama a função para descriptar a mensagem
                        resultado = await RSADecrypt(dataToEncrypt, false, objRSA);
                        // retorna mensagem descriptada
                        return ByteConverter.GetString(resultado);

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task<byte[]> RSAEncrypt(byte[] DataToEncrypt, bool DoOAEPPadding, RSACryptoServiceProvider objRSA)
        {
            try
            {
                // Informa as chaves 
                objRSA.FromXmlString(chavePublica);
                // devolve a mensagem encriptada
                return await Task.FromResult(objRSA.Encrypt(DataToEncrypt, DoOAEPPadding));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static async Task<byte[]> RSADecrypt(byte[] DataToDecrypt, bool DoOAEPPadding, RSACryptoServiceProvider objRSA)
        {
            try
            {
                // Informa as chaves 
                objRSA.FromXmlString(chavePrivada);
                // retorna mensagem descriptada
                return await Task.FromResult(objRSA.Decrypt(DataToDecrypt, DoOAEPPadding));
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static string ConverterRSAParametersParaXML(RSAParameters chave)
        {
            try
            {
                var sw = new System.IO.StringWriter();
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                xs.Serialize(sw, chave);
                return sw.ToString();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static RSAParameters ConverterXMLParaRSAParameters(string chave)
        {
            try
            {
                var sr = new System.IO.StringReader(chave);
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                return (RSAParameters)xs.Deserialize(sr);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string BuscarChaves() {

            using (RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider())
            {

                return "Publica " +
                 ConverterRSAParametersParaXML(objRSA.ExportParameters(false))
                 + "  |||||||||||| Privada " +
                  ConverterRSAParametersParaXML(objRSA.ExportParameters(true));

            }
        }

    }

}
