using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_Datos
{
    class Secundario
    {
        public string clave { get; set; }
        public List<long> direccion { get; set; }
        public Secundario()
        {
            direccion = new List<long>();
        }
    }
}
