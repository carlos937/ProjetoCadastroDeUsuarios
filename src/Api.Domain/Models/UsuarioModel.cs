using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class UsuarioModel:BaseModel
    {
        public string nome { get; set; }
        public string email { get; set; }
    }
}
