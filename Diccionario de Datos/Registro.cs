using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_Datos
{
    class Registro
    {
        #region Declaracion de Variables
        private long direccion;
        private List<string> atributos;
        private long dSig;
        #endregion
        public Registro()
        {
            direccion = -1;
            atributos = new List<string>();
            dSig = -1;

        }
        #region Metodos Get's y Set's
        public void direccionate(long dir)
        {
            direccion = dir;
        }
        public long dameDireccion()
        {
            return direccion;
        }
        public void ponteAtributo(string atributo)
        {
            atributos.Add(atributo);
        }
        public List<string> dameAtributos()
        {
            return atributos;
        }
        public void ponteDireccionSiguiente(long dir)
        {
            dSig = dir;
        }
        public long dameDireccionSiguiente()
        {
            return dSig;
        }

        #endregion

    }
}
