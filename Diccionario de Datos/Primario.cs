using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_Datos
{
    class Primario
    {
        public long dir { get; set; }
        public int  val { get; set; }
        public long dirVal { get; set; }
        public long dirSig { get; set; }

        public Primario()
        {
            dir = -1;
            val = 0;
            dirVal = -1;
            dirSig = -1;

        }
    }
}
