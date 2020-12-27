using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid id { get; set; }
        public DateTime dataDeCadastro { get; set; }
        public DateTime dataDeAtualizacao { get; set; }
    }
}
