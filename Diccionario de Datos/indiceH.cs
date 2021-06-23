using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HASH_V2
{
    class indiceH
    {
        public string cajon;
        public long apuntador;
        public Dictionary<string, long> dirDatos;

        public indiceH(string numC)
        {
            cajon = numC;
            apuntador = -1;
            dirDatos = new Dictionary<string, long>();
        }
    }
}
