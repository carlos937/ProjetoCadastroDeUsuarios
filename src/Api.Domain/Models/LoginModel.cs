using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class LoginModel:BaseModel
    {
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public double valorDajoice { get; set; }
        public JsonWebTokenModel jsonWebToken { get; set; }

    }
}
