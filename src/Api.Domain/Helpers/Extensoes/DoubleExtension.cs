using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers.Extensoes
{
    public static class DoubleExtension
    {
        public static string FormatarValor(this double valor)
        {
            return valor.ToString("0.##");
        }
    }
}
