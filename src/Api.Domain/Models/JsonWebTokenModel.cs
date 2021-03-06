using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class JsonWebTokenModel
    {
        public string created { get; set; }
        public string expiration { get; set; }
        public string token { get; set; }
    }
}
