
using System;

namespace Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string nome { get; set; }
        public string email { get; set; }
    }
}
