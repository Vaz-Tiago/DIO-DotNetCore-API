using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIO.CursosAPI.Models
{
    public class ValidaCamposViewModelOutput
    {
        public IEnumerable<string> Erros { get; set; }

        public ValidaCamposViewModelOutput(IEnumerable<string> erros)
        {
            Erros = erros;
        }
    }
}
