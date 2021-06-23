using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_Datos
{
    class Arbol : List<Nodo>
    {
        public int renglon { get; set; }
        public int tam { get; set; }
        public int valor { get; set; }

        public Arbol()
        {



        }
        public Nodo creaNodo(Nodo nuevo, long DN)
        {
            nuevo.tipo = 'H';
            nuevo.dirNodo = DN;
            nuevo.sig = -1;
            nuevo.clave = new List<int>();
            nuevo.direccion = new List<long>();

            return nuevo;
        }
        public long dameRaiz()
        {
            foreach (Nodo n in this)
            {
                if (n.tipo == 'R')
                {
                    return n.dirNodo;
                }
            }
            return -1;
        }
        public Nodo dameNodo(long dir)
        {
            Nodo aux = new Nodo();
            foreach (Nodo np in this)
            {
                if (np.dirNodo == dir)
                {
                    aux = np;
                }
            }
            return aux;
        }
        public Nodo dameNodo(long dir, long no)
        {
            Nodo aux = new Nodo();
            foreach (Nodo np in this)
            {
                if (np.dirNodo == dir && np.dirNodo != no)
                {
                    aux = np;
                }
            }
            return aux;
        }
        public bool existeIntermedio()
        {
            bool band = false;
            foreach (Nodo n in this)
            {
                if (n.tipo == 'I')
                {
                    band = true;
                    return band;
                }
            }
            return band;
        }
        public int totalIntermedio()
        {
            int total = 0;
            foreach (Nodo n in this)
            {
                if (n.tipo == 'I')
                    total++;

            }
            return total;
        }
        public long dameIntermedio(int clave)
        {
            long dir = -1;
            foreach (Nodo n in this)
            {
                if (n.tipo == 'R')
                {
                    for (int i = 0; i < n.clave.Count; i++)
                    {
                        if (clave < n.clave[i])
                        {
                            dir = n.direccion[i];

                            return dir;

                        }
                        else if (i == n.clave.Count - 1)
                        {
                            dir = n.direccion[i + 1];
                            return dir;
                        }
                    }
                }
            }

            return dir;
        }
        public long buscaNodo(int clave)
        {
            bool intermedio = false;
            intermedio = existeIntermedio();
            int total = totalIntermedio();
            int cont = 0;
            Nodo inter, inter2 = new Nodo();
            foreach (Nodo n in this)
            {
                if (n.tipo == 'R' && intermedio == false)
                {
                    for (int i = 0; i < n.clave.Count; i++)
                    {
                        if (clave < n.clave[i])
                        {

                            return n.direccion[i];
                        }

                        else if (i == n.clave.Count - 1) // if (i == n.clave.Count - 1) 
                        {
                            if (i == n.direccion.Count - 1) // (i == n.direccion.Count)
                            {
                             //   n.sig = n.direccion[n.direccion.Count - 1];
                                return n.direccion[i+1]; // i 
                            }
                                
                            else
                                try
                                {
                                    return n.direccion[i + 1];// n.direccion[i + 1];
                                }
                                catch { return n.sig; }//n.sig;
                                //return n.sig;
                            //return n.direccion[i+1]; // i 
                        }


                    }
                }
                else if (n.tipo == 'I' && intermedio == true)
                {
                    inter = dameNodo(dameRaiz());
                    cont++;
                    for (int i = 0; i < inter.clave.Count; i++)
                    {
                        if (clave < inter.clave[i])
                        {
                            //inter2 = inter;
                            inter2 = dameNodo(inter.direccion[i]);
                            break;
                        }
                        else if (i == inter.clave.Count - 1)
                        {
                            inter2 = dameNodo(inter.direccion[i + 1]);
                            break;
                        }



                    }
                    if (inter2.clave.Count > 0)
                    {
                        for (int i = 0; i < inter2.clave.Count; i++)
                        {
                            if (clave < inter2.clave[i])
                            {

                                return inter2.direccion[i];
                            }
                            
                            else if (i == n.clave.Count - 1)
                            {
                                if (i == n.direccion.Count - 1)
                                    return n.direccion[i]; // i 
                                else
                                    return n.direccion[i + 1];
                            }


                        }
                    }

                }
            }

            /*
            bool intermedio = false;
            intermedio = existeIntermedio();
            List<int> ClavesIntermedio = new List<int>();
            List<long> DirIntermedio = new List<long>();
            int total = totalIntermedio();
            int cont = 0;
            long dirInter = 0;
            Nodo inter, inter2 = new Nodo();
            if (dameRaiz() == -1)
            {
                return -1;
            }
            else if(intermedio == false && dameRaiz() != -1 )  // Hay raiz pero no hay intermedio
            {
                foreach (Nodo n in this)
                {
                    if (n.tipo == 'R')
                    {
                        for (int i = 0; i < n.clave.Count; i++)
                        {
                            if (clave < n.clave[i])
                            {
                                return n.direccion[i];
                            }
                            else if (i == n.clave.Count - 1)
                            {
                                return n.direccion[i + 1];
                            }
                        }
                    }
                }
            }
            else if(intermedio == true && dameRaiz() != -1 )
            {
                foreach (Nodo n in this)
                {
                    if (n.tipo == 'R')
                    {
                        for (int i = 0; i < n.clave.Count; i++)
                        {
                            if (clave < n.clave[i])
                            {
                                dirInter = n.direccion[i];
                                inter = dameNodo(dirInter);
                                for (int j = 0; j < inter.clave.Count; j++)
                                {
                                    if (clave < inter.clave[j])
                                    {
                                        return inter.direccion[j];
                                    }
                                    else if (j == inter.clave.Count - 1)
                                    {
                                        return inter.direccion[j+1];
                                    }
                                }
                            }
                            else if (i == n.clave.Count - 1)
                            {
                                dirInter = n.direccion[i + 1];
                                inter = dameNodo(dirInter);
                                for (int j = 0; j < inter.clave.Count; j++)
                                {
                                    if (clave < inter.clave[j])
                                    {
                                        return inter.direccion[j];
                                    }
                                    else if (j == inter.clave.Count - 1)
                                    {
                                        return inter.direccion[j + 1];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            /*
            foreach(Nodo n in this)
            {
                if(intermedio == false  && n.tipo == 'R' )
                {
                    for(int i = 0; i < n.clave.Count; i++)
                    {
                        if (clave < n.clave[i])
                        {
                            return n.direccion[i];
                        }
                        
                        else if(i == n.clave.Count-1)
                        {
                            return n.direccion[i + 1];
                        }
                        
                    }
                }
                else if (intermedio == true && n.tipo == 'I')
                {
                    inter = dameNodo(dameRaiz());  // Nodo Raiz donde se busca el nodo intermedio
                    cont++;
                    for(int i = 0; i < inter.clave.Count; i++)
                    {
                        if(clave < inter.clave[i])
                        {
                            //inter2 = inter;
                            inter2 = dameNodo(inter.direccion[i]);
                           // break;
                        }
                        else if (i == inter.clave.Count)
                        {
                            inter2 = dameNodo(inter.direccion[i + 1]);
                           // break;
                        }    
                    }
                    if(inter2.clave.Count > 0)
                    {
                        for (int i = 0; i < inter2.clave.Count; i++)
                        {
                            if (clave < inter2.clave[i])
                            {
                                return inter2.direccion[i];
                            }
                            else if (i == inter2.clave.Count-1)
                            {
                                return inter2.direccion[i+1];
                            }
                        }
                    }
                    
                }
            }
            return -1;
            */

            return -1;
        }
        public long dameUltimoNodo()
        {
            foreach (Nodo np in this)
            {
                if (np.tipo != 'R' && np.sig == -1)
                    return np.dirNodo;
            }
            return -1;
        }
        public void ordenaIntermedio(long dir)
        {

            Nodo arr = dameNodo(dir);
            int clave;
            for (int i = 0; i < arr.clave.Count - 1; i++)
            {
                if (arr.clave[i] > arr.clave[i + 1])
                {
                    clave = arr.clave[i];
                    arr.clave[i] = arr.clave[i + 1];
                    arr.clave[i + 1] = clave;

                    dir = arr.direccion[i + 1];
                    // if(i+2 < arr.direccion.Count)
                    //  {
                    arr.direccion[i + 1] = arr.direccion[i + 2];
                    arr.direccion[i + 2] = dir;
                    //  }
                    //  else
                    //  {
                    //      arr.direccion[i + 1] = arr.direccion[i + 1];
                    //      arr.direccion[i + 1] = dir;
                    //  }


                }
            }

        }
        public void ordenaRaiz()
        {
            Nodo arr = dameNodo(dameRaiz());
            int clave;
            long dir;
            for (int i = 0; i < arr.clave.Count - 1; i++)
            {
                if (arr.clave[i] > arr.clave[i + 1])
                {
                    clave = arr.clave[i];
                    arr.clave[i] = arr.clave[i + 1];
                    arr.clave[i + 1] = clave;

                    dir = arr.direccion[i + 1];
                    arr.direccion[i + 1] = arr.direccion[i + 2];
                    arr.direccion[i + 2] = dir;

                }
            }


        }
        public bool agregaDato(long direccion, int clave, long apuntador)
        {
            int g;
            int pos = 0;
            long raiz = dameRaiz();
            List<int> tempClave = new List<int>();
            List<long> tempDir = new List<long>();
            foreach (Nodo np in this)
            {
                if (np.dirNodo == direccion)
                {
                    if (np.clave.Count < 4)
                    {

                        if (raiz == direccion)
                        {
                            np.clave.Add(clave);
                            np.direccion.Add(apuntador);
                            ordenaRaiz();
                        }
                        else if (np.tipo == 'I')
                        {
                            np.clave.Add(clave);
                            np.direccion.Add(apuntador);
                            ordenaIntermedio(direccion);
                        }
                        else
                        {
                            np.clave.Add(clave);
                            np.direccion.Add(apuntador);
                            ordenaNodo(np);

                        }

                        return false;

                    }
                    else if (np.clave.Count == 4) // Aqui se reestructura el nodo
                    {

                        return true;

                    }

                }
            }
            return false;
        }
        public void ordenaNodo(Nodo np)
        {
            int i, j, k;
            long k2;
            for (i = np.clave.Count - 1; i > 0; i--)
                for (j = 0; j < i; j++)
                    if (np.clave[j] > np.clave[j + 1])
                    {
                        k = np.clave[j];
                        np.clave[j] = np.clave[j + 1];
                        np.clave[j + 1] = k;

                        k2 = np.direccion[j];
                        np.direccion[j] = np.direccion[j + 1];
                        np.direccion[j + 1] = k2;
                    }
        }
        public void ordenaCinco(List<int> tempClave, List<long> tempDir)
        {
            int i, j, k;
            long k2;
            for (i = tempClave.Count - 1; i > 0; i--)
                for (j = 0; j < i; j++)
                    if (tempClave[j] > tempClave[j + 1])
                    {
                        k = tempClave[j];
                        tempClave[j] = tempClave[j + 1];
                        tempClave[j + 1] = k;

                        k2 = tempDir[j];
                        tempDir[j] = tempDir[j + 1];
                        tempDir[j + 1] = k2;
                    }
        }


        #region ELIMINA CLAVES
        // ALGORITMOS PARA LA ELIMINACIÓN
        public void actualizaRaizDerecho(long derecho)
        {
            Nodo der = dameNodo(derecho);
            Nodo nRaiz = dameNodo(dameRaiz());
            for(int i =0; i < nRaiz.direccion.Count; i++)
            {
                if(nRaiz.direccion[i+1] == derecho)
                {
                    nRaiz.clave[i] = der.clave[i+1];
                    break;
                }
            }
        }
        public void actualizaRaizIzquierdo(long izquierda)
        {
            Nodo izq = dameNodo(izquierda);
            Nodo nRaiz = dameNodo(dameRaiz());
            for(int i = 0; i < nRaiz.direccion.Count; i++)
            {
                if(nRaiz.direccion[i] == izquierda)
                {
                    nRaiz.clave[i] = izq.clave[izq.clave.Count-1];
                    break;
                }
            }
        }
        public bool eliminaNodo(long dNodo, int clave, long dClave)
        {
            bool band = false;
            int pos = 0;
            foreach (Nodo aux in this)
            {
                if (aux.dirNodo == dNodo)
                {
                    foreach (int i in aux.clave)
                    {
                        if (clave == i)
                        {
                            pos = damePosicion(clave, aux.dirNodo);
                            aux.clave.Remove(i);
                            aux.direccion.RemoveAt(pos);
                            if (aux.clave.Count < 2)
                            {
                                band = true;
                                return band;
                            }
                            else
                                return false;
                        }



                    }
                }
            }
            return band;

        }
        private int damePosicion(int clave, long dir)
        {
            int i = 0;
            foreach (Nodo aux in this)
            {
                if (aux.dirNodo == dir)
                {
                    foreach (int cont in aux.clave)
                    {

                        if (clave == cont)
                        {
                            return i;
                        }
                        i++;
                    }
                }

            }
            return 0;
        }
        public long buscaNodoE(int clave)
        {
            bool intermedio = false;
            intermedio = existeIntermedio();
            int total = totalIntermedio();
            int cont = 0;
            Nodo inter, inter2 = new Nodo();
            foreach (Nodo n in this)
            {
                if (n.tipo == 'R' && intermedio == false)
                {
                    for (int i = 0; i < n.clave.Count; i++)
                    {
                        if (clave < n.clave[i])
                        {

                            return n.direccion[i];
                        }

                        else if (i == n.clave.Count - 1)
                        {
                            if (i == n.direccion.Count-1)
                                return n.direccion[i]; // i 
                            else
                                return n.direccion[i + 1];
                        }


                    }
                }
                else if (n.tipo == 'I' && intermedio == true)
                {
                    inter = dameNodo(dameRaiz());
                    cont++;
                    for (int i = 0; i < inter.clave.Count; i++)
                    {
                        if (clave < inter.clave[i])
                        {
                            //inter2 = inter;
                            inter2 = dameNodo(inter.direccion[i]);
                            break;
                        }
                        else if (i == inter.clave.Count - 1)
                        {
                            inter2 = dameNodo(inter.direccion[i + 1]);
                            break;
                        }



                    }
                    if (inter2.clave.Count > 0)
                    {
                        for (int i = 0; i < inter2.clave.Count; i++)
                        {
                            if (clave < inter2.clave[i])
                            {

                                return inter2.direccion[i];
                            }

                            else if (i == inter2.clave.Count - 1)
                            {
                                return inter2.direccion[i + 1];
                            }


                        }
                    }

                }
            }

            /*
            bool intermedio = false;
            intermedio = existeIntermedio();
            List<int> ClavesIntermedio = new List<int>();
            List<long> DirIntermedio = new List<long>();
            int total = totalIntermedio();
            int cont = 0;
            long dirInter = 0;
            Nodo inter, inter2 = new Nodo();
            if (dameRaiz() == -1)
            {
                return -1;
            }
            else if(intermedio == false && dameRaiz() != -1 )  // Hay raiz pero no hay intermedio
            {
                foreach (Nodo n in this)
                {
                    if (n.tipo == 'R')
                    {
                        for (int i = 0; i < n.clave.Count; i++)
                        {
                            if (clave < n.clave[i])
                            {
                                return n.direccion[i];
                            }
                            else if (i == n.clave.Count - 1)
                            {
                                return n.direccion[i + 1];
                            }
                        }
                    }
                }
            }
            else if(intermedio == true && dameRaiz() != -1 )
            {
                foreach (Nodo n in this)
                {
                    if (n.tipo == 'R')
                    {
                        for (int i = 0; i < n.clave.Count; i++)
                        {
                            if (clave < n.clave[i])
                            {
                                dirInter = n.direccion[i];
                                inter = dameNodo(dirInter);
                                for (int j = 0; j < inter.clave.Count; j++)
                                {
                                    if (clave < inter.clave[j])
                                    {
                                        return inter.direccion[j];
                                    }
                                    else if (j == inter.clave.Count - 1)
                                    {
                                        return inter.direccion[j+1];
                                    }
                                }
                            }
                            else if (i == n.clave.Count - 1)
                            {
                                dirInter = n.direccion[i + 1];
                                inter = dameNodo(dirInter);
                                for (int j = 0; j < inter.clave.Count; j++)
                                {
                                    if (clave < inter.clave[j])
                                    {
                                        return inter.direccion[j];
                                    }
                                    else if (j == inter.clave.Count - 1)
                                    {
                                        return inter.direccion[j + 1];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            /*
            foreach(Nodo n in this)
            {
                if(intermedio == false  && n.tipo == 'R' )
                {
                    for(int i = 0; i < n.clave.Count; i++)
                    {
                        if (clave < n.clave[i])
                        {
                            return n.direccion[i];
                        }
                        
                        else if(i == n.clave.Count-1)
                        {
                            return n.direccion[i + 1];
                        }
                        
                    }
                }
                else if (intermedio == true && n.tipo == 'I')
                {
                    inter = dameNodo(dameRaiz());  // Nodo Raiz donde se busca el nodo intermedio
                    cont++;
                    for(int i = 0; i < inter.clave.Count; i++)
                    {
                        if(clave < inter.clave[i])
                        {
                            //inter2 = inter;
                            inter2 = dameNodo(inter.direccion[i]);
                           // break;
                        }
                        else if (i == inter.clave.Count)
                        {
                            inter2 = dameNodo(inter.direccion[i + 1]);
                           // break;
                        }    
                    }
                    if(inter2.clave.Count > 0)
                    {
                        for (int i = 0; i < inter2.clave.Count; i++)
                        {
                            if (clave < inter2.clave[i])
                            {
                                return inter2.direccion[i];
                            }
                            else if (i == inter2.clave.Count-1)
                            {
                                return inter2.direccion[i+1];
                            }
                        }
                    }
                    
                }
            }
            return -1;
            */

            return 0;
        }

        public long dameNodoIntermedio(long dir)
        {
            long direccion = -1;
            foreach (Nodo aux in this)
            {
                if (aux.tipo == 'I')
                {
                    foreach (long ap in aux.direccion)
                    {
                        if (ap == dir)
                        {
                            direccion = aux.dirNodo;
                            return direccion;
                        }
                    }

                }
            }
            return direccion;
        }
        public long existeIntermedioDerecho(long inter)
        {
            long dInterDer = -1;
            Nodo nRaiz = dameNodo(dameRaiz());
            for (int i = 0; i < nRaiz.direccion.Count - 1; i++)
            {
                if (nRaiz.direccion[i] == inter)
                {
                    dInterDer = nRaiz.direccion[i + 1];
                    return dInterDer;
                }
            }

            return dInterDer;
        }
        public long existeIntermedioIzquierdo(long inter)
        {
            long dInterIzq = -1;
            Nodo nRaiz = dameNodo(dameRaiz());
            for (int i = 0; i < nRaiz.direccion.Count; i++)
            {
                if (nRaiz.direccion[i] == inter)
                {
                    dInterIzq = nRaiz.direccion[i - 1];
                    return dInterIzq;
                }
            }
            return dInterIzq;
        }
        public long existeizquierdo(long dInter, long dn)
        {
            long dIzq = -1;
            Nodo inter = dameNodo(dInter);
            for (int i = 0; i < inter.direccion.Count; i++)
            {
                if (inter.direccion[i] == dn)
                {
                    if (i == 0)
                    {
                        dIzq = inter.direccion[i];
                        return dIzq;
                    }
                    else
                    {
                        dIzq = inter.direccion[i - 1];
                        return dIzq;
                    }

                }
            }
            return dIzq;
        }
        public long existeDerecho(long dInter, long dn)
        {
            long derecho = -1;
            Nodo inter = dameNodo(dInter);
            for (int i = 0; i < inter.direccion.Count - 1; i++)
            {
                if (inter.direccion[i] == dn)
                {
                    derecho = inter.direccion[i + 1];
                    return derecho;
                }
            }
            return derecho;
        }
        public long pideDerecho(long dInter, long dn)
        {
            long direccion = -1;
            Nodo derecho = new Nodo();
            Nodo inter = dameNodo(dInter);
            for (int i = 0; i < inter.direccion.Count - 1; i++)
            {
                if (inter.direccion[i] == dn)
                {
                    if (dameNodo(inter.direccion[i + 1]).clave.Count > 2)
                    {
                        direccion = inter.direccion[i + 1];
                        return direccion;
                    }
                }
            }

            return direccion;
        }
        public long pideIzquierdo(long dInter, long dn)
        {
            long direccion = -1;
            Nodo izquierdo = new Nodo();
            Nodo inter = dameNodo(dInter);
            for (int i = 0; i < inter.direccion.Count; i++)
            {
                if (inter.direccion[i] == dn)
                {
                    if (i != 0)
                    {
                        if (dameNodo(inter.direccion[i - 1]).clave.Count > 2)
                        {
                            direccion = inter.direccion[i - 1];
                            return direccion;
                        }
                    }

                }
            }

            return direccion;
        }
        public long pideIntermedioIzquierdo(long dRaiz, long dn)
        {
            long direccion = -1;
            Nodo izquierdo = new Nodo();
            Nodo inter = dameNodo(dameRaiz());
            for (int i = 0; i < inter.direccion.Count; i++)
            {
                if (inter.direccion[i] == dn)
                {
                    if (dameNodo(inter.direccion[i - 1]).clave.Count > 2)
                    {
                        direccion = inter.direccion[i - 1];
                        return direccion;
                    }
                }
            }

            return direccion;
        }
        public long pideIntermedioDerecho(long dInter, long dn)
        {
            long direccion = -1;
            Nodo derecho = new Nodo();
            Nodo inter = dameNodo(dameRaiz());
            for (int i = 0; i < inter.direccion.Count - 1; i++)
            {
                if (inter.direccion[i] == dn)
                {
                    if (dameNodo(inter.direccion[i + 1]).clave.Count > 2)
                    {
                        direccion = inter.direccion[i + 1];
                        return direccion;
                    }
                }
            }

            return direccion;
        }

        public bool fusionaIntermedioIzquierda(long inter, long izquierda)
        {
            bool band = false;
            Nodo intermedio = dameNodo(inter);
            Nodo nRaiz = dameNodo(dameRaiz());
            Nodo nIzquierda = dameNodo(izquierda);
            long izq, der;
            /*
            List<int> tempClaves = new List<int>();
            List<long> tempDir = new List<long>();
            foreach(int i in intermedio.clave)
            {
                tempClaves.Add(i);

            }
            foreach(long l in intermedio.direccion)
            {
                tempDir.Add(l);
            }
            foreach(int i in nIzquierda.clave)
            {
                tempClaves.Add(i);
            }
            foreach (long l in nIzquierda.direccion)
            {
                tempDir.Add(l);
            }
           

            if (nRaiz.clave.Count == 1)
            {
              //  tempDir.Add(nRaiz.direccion[1]);
              //  tempDir.Add(nRaiz.direccion[0]);
                tempClaves.Add(nRaiz.clave[0]);
            }
            ordenaCinco(tempClaves, tempDir);
            */
            nIzquierda.clave.Add(nRaiz.clave[0]);
            nIzquierda.clave.Add(intermedio.clave[0]);
            nIzquierda.direccion.Add(intermedio.direccion[0]);
            nIzquierda.direccion.Add(intermedio.direccion[1]);
            if (nIzquierda.clave.Count == 4)
            {
                nIzquierda.sig = nIzquierda.direccion[nIzquierda.direccion.Count - 1];
            }
            //ELIMINA LOS DATOS DE NODO IZQUIERDO
            intermedio.clave.RemoveAt(0);
            //intermedio.clave.RemoveAt(0);
            intermedio.direccion.RemoveAt(1);
            intermedio.direccion.RemoveAt(0);
            // ELIMINA DE RAIZ
            //ordenaCinco(tempClaves, tempDir);
            bool band2 = eliminaRaizIzquierdo(inter, izquierda);
            if (band2 == true)
            {
                Nodo R = dameNodo(dameRaiz());
                this.Remove(R);
                this.Remove(intermedio);
                return true;
            }
            else
            {
                return false;
            }



        }
        public bool fusionIzquierda(long actual, long izquierda, long inter)
        {
            bool band = false;
            Nodo nActual = dameNodo(actual);
            Nodo nIzquierda = dameNodo(izquierda);
            Nodo intermedio = dameNodo(inter);
            long izq, der;
            for (int i = 0; i < nActual.clave.Count; i++)
            {
                agregaDato(izquierda, nActual.clave[i], nActual.direccion[i]);
            }
            // ELIMINA LOS DATOS DEL NODO DERECHO
            nActual.clave.RemoveAt(0);
            nActual.direccion.RemoveAt(0);
            // nActual.direccion.RemoveAt(0);
            //elimina del nodo intermedio
            bool band2 = eliminaIntermedio(inter, actual);
            if (nActual.clave.Count == 0)
            {
                this.Remove(nActual);
            }
            if (band2 == true)
            {
                Remove(intermedio);
                return true;
            }
            else
                return false;

        }
        public bool fusionDerecha(long actual, long derecha, long inter)
        {
            bool band = false;
            Nodo nActual = dameNodo(actual);
            Nodo dDerecha = dameNodo(derecha);
            Nodo intermedio = dameNodo(inter);
            long izquierda, der;
            for (int i = 0; i < dDerecha.clave.Count; i++)
            {
                agregaDato(actual, dDerecha.clave[i], dDerecha.direccion[i]);
            }
            // ELIMINA LOS DATOS DEL NODO DERECHO
            dDerecha.clave.RemoveAt(1);
            dDerecha.clave.RemoveAt(0);
            dDerecha.direccion.RemoveAt(1);
            dDerecha.direccion.RemoveAt(0);
            //elimina del nodo intermedio
            bool band2 = eliminaIntermedio(inter, derecha);
            if (band2 == true)
            {
                return true;
            }
            else
                return false;

        }
        public bool eliminaRaizIzquierdo(long inter, long izquierdo)
        {
            Nodo nRaiz = dameNodo(dameRaiz());
            int c = 0;
            foreach (long cont in nRaiz.direccion)
            {
                if (cont == inter)
                {
                    nRaiz.clave.RemoveAt(c - 1);
                    nRaiz.direccion.RemoveAt(c);
                    break;
                }
                c++;
            }
            if (nRaiz.clave.Count == 0)
            {
                Remove(nRaiz);
                return true;
            }
            else
                return false;
        }
        public bool eliminaIntermedio(long inter, long derecha)
        {
            Nodo intermedio = dameNodo(inter);
            int c = 0;
            foreach (long cont in intermedio.direccion)
            {
                if (cont == derecha)
                {
                    if (c == 0)
                    {
                        intermedio.clave.RemoveAt(c);
                        intermedio.direccion.RemoveAt(c);
                        break;
                    }
                    else
                    {
                        intermedio.clave.RemoveAt(c - 1);
                        intermedio.direccion.RemoveAt(c);
                        break;
                    }

                }
                c++;
            }

            if (intermedio.clave.Count < 2)
            {
                return true;
            }
            else
                return false;

        }
        public void eliminaVacios()
        {
            foreach(Nodo np in this)
            {
                if(np.clave.Count == 0)
                {
                    Remove(np);
                    break;
                }
            }
        }
        public void eliminaCeros()
        {

            foreach (Nodo np in this)
            {
                np.clave = np.clave.Distinct().ToList();
                np.direccion = np.direccion.Distinct().ToList();
               
                foreach(long i in np.direccion)
                {
                    if ( i == 0 )
                    {
                        if(np.direccion[0] != 0)
                        {
                            np.direccion.Remove(i);
                            break;
                        }
                           
                    }
               }
                    
                foreach (int i in np.clave)
                {
                    if (i == 0)
                    {
                        np.clave.Remove(i);
                        break;
                    }
                }

               if(np.direccion[np.direccion.Count-1] == np.sig)
                {
                    np.direccion.RemoveAt(np.direccion.Count-1);
                }
                
            }
            
        }

        #endregion


    }
}
