using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_Datos
{
    class ListaSecundario
    {
        public string nombre { get; set; }
        public int    posTabla { get; set; }
        public int posArchivo { get; set; }
        public ListaSecundario()
        {
            nombre = " ";
            posTabla = 0;
            posArchivo = 0;
        }
    }
}
