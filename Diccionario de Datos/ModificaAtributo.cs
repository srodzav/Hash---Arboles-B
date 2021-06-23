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
    partial class ModificaAtributo : Form
    {
        private List<Atributo> atri = new List<Atributo>();
        private Atributo nuevoA = new Atributo();
        private long dir;
        private long dSig;
        private long dIndice;
       
        public ModificaAtributo(List<Atributo> a)
        {
            InitializeComponent();
            foreach (Atributo aux in a)
            {
                atri.Add(aux);
                comboBox1.Items.Add(aux.dameNombre());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            foreach(Atributo atributo in atri)
            {
                if(comboBox1.Text == atributo.dameNombre())
                {
                    textBox1.Text = atributo.dameNombre();
                    textBox2.Text = atributo.dameTipo().ToString();
                    textBox3.Text = atributo.dameLongitud().ToString();
                    textBox4.Text = atributo.dameTI().ToString();
                    dir = atributo.dameDireccion();
                    dSig = atributo.dameDirSig();
                    dIndice = atributo.dameDirIndice();
                }
                
            }
        }
        public Atributo dameNuevo()
        {
            nuevoA.nombrate(textBox1.Text);
            nuevoA.ponteTipo(Convert.ToChar(textBox2.Text));
            nuevoA.ponteLongitud(Convert.ToInt32(textBox3.Text));
            nuevoA.direccionate(dir);
            nuevoA.ponteTipoIndice(Convert.ToInt32(textBox4.Text));
            nuevoA.ponteDirIndice(dIndice);
            nuevoA.ponteDirSig(dSig);
            return nuevoA;
        }
    }
}
