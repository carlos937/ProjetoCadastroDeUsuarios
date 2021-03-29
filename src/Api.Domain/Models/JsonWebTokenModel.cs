using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class JsonWebTokenModel
    {
        public DateTime created { get; set; }
        public DateTime expiration { get; set; }
        public string token { get; set; }
    }
}
