using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diccionario_de_Datos
{
    public partial class ModificaRegistro : Form
    {
        private List<string> cb = new List<string>();
        DataGridView temporal = new DataGridView();
        private string nArchivo;
        public ModificaRegistro(DataGridView tabla, DataGridView regis, List<string> claves)
        {
            InitializeComponent();
            for (int i = 0; i < tabla.Columns.Count; i++)
            {
                dataGridView3.Columns.Add(tabla.Columns[i].Name.ToString(), tabla.Columns[i].Name.ToString());
            }
            foreach (string s in claves)
            {
                comboBox1.Items.Add(s);
            }
            cb = claves;
        }
        public string dameClave()
        { return comboBox1.Text; }
        public DataGridView dameRenglon()
        { return dataGridView3; }
        private void ModificaRegistro_Load(object sender, EventArgs e)
        { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        { }
        private void button9_Click(object sender, EventArgs e)
        { this.Close(); }
        private void button1_Click(object sender, EventArgs e)
        { this.Close(); }
    }
}
