using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diccionario_de_Datos
{
    public static class MetodosRegistros
    {
        // BUSCA LA DIRECCIÓN DEL NODO QUE APUNTA AL NODO QUE SE VA A MODIFICAR
        public static long buscaAnteriorActual(DataGridView dataGridView4, string actual, long DR, int clave)
        {
            int i;
            long direccion;
            direccion = -1;
            for (i = 0; i < dataGridView4.Rows.Count - 2; i++)
            {
                if(DR.ToString() == dataGridView4.Rows[i].Cells[dataGridView4.Columns.Count-1].Value.ToString())
                {
                    direccion = Convert.ToInt64(dataGridView4.Rows[i].Cells[0].Value);
                    break;
                }
            }

            return direccion;
        }

        // BUSCA LA DIRECCIÓN DEL NODO SIGUIENTE DEL NODO QUE SE VA A MODIFICAR
        public static long buscaSiguienteActual(DataGridView dataGridView4, string actual, long DR, int clave)
        {
            int i;
            long direccion;
            direccion = -1;
            for (i = 0; i < dataGridView4.Rows.Count - 2; i++)
            {
                if (DR.ToString() == dataGridView4.Rows[i].Cells[0].Value.ToString())
                {
                    direccion = Convert.ToInt64(dataGridView4.Rows[i].Cells[dataGridView4.Columns.Count-1].Value);
                    break;
                }
            }

            return direccion;
        }
        // BUSCA LA DIRECCIÓN ANTERIOR DEL NODO NUEVO
        public static long buscaAnteriorNuevo(DataGridView dataGridView4, string actual, long DR, int clave)
        {
            int i, j;
            int compara = 0;
            long direccion = -1;
            for (i = 0; i < dataGridView4.Rows.Count - 2; i++)
            {
                compara = actual.CompareTo(dataGridView4.Rows[i].Cells[clave + 1].Value.ToString());
                if (compara == -1)
                {
                    if (i == 0)
                    {
                        direccion =  Convert.ToInt64(dataGridView4.Rows[i].Cells[0].Value);
                    }
                    else
                        direccion = Convert.ToInt64(dataGridView4.Rows[i - 1].Cells[0].Value);
                    return direccion;
                }
                if(compara == 0) // SI SE REPITE LA CLAVE DE BÚSQUEDA 
                {
                    direccion = Convert.ToInt64(dataGridView4.Rows[i - 1].Cells[0].Value);
                    return direccion;
                }
            }
            return direccion;
        }
        // BUSCA LA DIRECCIÓN SIGUIENTE DEL NODO NUEVO
        public static long buscaSiguienteNuevo(DataGridView dataGridView4, string actual, long DR, int clave)
        {
            int i, j;
            int compara = 0;
            long direccion = -1;
            for (i = 0; i < dataGridView4.Rows.Count - 2; i++)
            {
                compara = actual.CompareTo(dataGridView4.Rows[i].Cells[clave + 1].Value.ToString());
                if (compara == -1)
                {
                    if (i == 0)
                    {
                        direccion =  Convert.ToInt64(dataGridView4.Rows[i].Cells[0].Value);
                    }
                    else
                        direccion = Convert.ToInt64(dataGridView4.Rows[i - 1].Cells[dataGridView4.Columns.Count-1].Value);
                    return direccion;
                }
                if (compara == 0) // SI SE REPITE LA CLAVE DE BÚSQUEDA 
                {
                    direccion = Convert.ToInt64(dataGridView4.Rows[i].Cells[0].Value);
                    return direccion;
                }
            }
            return direccion;
        }
        // BUSCA EL ULTIMO REGISTRO DE LA LISTA
        public static long buscaUltimoRegistro(DataGridView dataGridView4)
        {
            int i, j;
            long direccion = -1;
            for (i = 0; i < dataGridView4.Rows.Count - 2; i++)
            {
                if(Convert.ToInt64(dataGridView4.Rows[i].Cells[dataGridView4.Columns.Count-1].Value) == -1)
                {
                    direccion = Convert.ToInt64(dataGridView4.Rows[i].Cells[0].Value);
                    break;
                }

            }
            return direccion;
        }
    }
}
