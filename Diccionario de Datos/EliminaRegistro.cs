using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diccionario_de_Datos
{
    public partial class EliminaRegistro : Form
    {
        private List<string> registros;
        public EliminaRegistro(List <string> claves)
        {
            InitializeComponent();
            registros = claves;
            foreach(string reg in registros)
            {
                comboBox1.Items.Add(reg);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        public string dameClave()
        {
            return comboBox1.Text;
        }
    }
}
