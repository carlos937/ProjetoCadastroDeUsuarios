using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class BaseModel:ServerStatus
    {
        public Guid id { get; set; }
        public DateTime dataDeCadastro { get; set; }
        public DateTime dataDeAtualizacao { get; set; }
        public bool lixeira { get; set; }
        public string created { get; set; } 
        public string expiration { get; set; } 
        public string token { get; set; }
    }
}
