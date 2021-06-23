using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_Datos
{
    class Nodo
    {
        public char tipo { get; set; }
        public long dirNodo { get; set; }
        public List<int> clave { get; set; }
        public List<long> direccion { get; set; }
        public long sig { get; set; }



        public struct nodos
        {
            public List<int> lc;
            public List<long> ld;
        }
        public nodos claves { get; set; }

        public Nodo()
        {
            dirNodo = -1;
            tipo = 'H';
            clave = new List<int>();
            direccion = new List<long>();
            
            sig = -1;
        }

        
    }
}
