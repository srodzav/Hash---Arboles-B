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
    public partial class EliminaAtributo : Form
    {
        private List<string> entidad = new List<string>();
        private string archivo;
        private BinaryReader br;
        public string nEntidad { get; set; }
        public string nAtributo { get; set; }
        public long dAtributo { get; set; }
        private long dEntidad;
        public EliminaAtributo(List<string> entidad, string archivo)
        {
            InitializeComponent();
            this.entidad = entidad;
            this.archivo = archivo;
            dAtributo = -1;
            foreach(string s in entidad)
            { comboBox1.Items.Add(s); }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            long cab, DSIG = 0, DAT = 0;
            string n;
            br = new BinaryReader(File.Open(archivo, FileMode.Open));
            cab = br.ReadInt64();
            br.BaseStream.Position = cab;
            while(cab < br.BaseStream.Length)
            {
                if(DSIG != -1)
                {
                    n = br.ReadString();
                    dEntidad = br.ReadInt64();
                    DAT = br.ReadInt64();
                    br.ReadInt64();
                    DSIG = br.ReadInt64();
                    if(n == comboBox1.Text)
                    {
                        cab = DAT;
                        nEntidad = n;
                        break;
                    }
                }
                br.BaseStream.Position = DSIG;
                cab = DSIG;  
            }
            if(cab != -1)
            {
                br.BaseStream.Position = cab;
                DSIG = 0;
                while (cab < br.BaseStream.Length)
                {
                    if (DSIG != -1)
                    {
                        nAtributo = br.ReadString(); 
                        comboBox2.Items.Add(nAtributo);
                        br.ReadChar();                     
                        br.ReadInt32();                  
                        dAtributo= br.ReadInt64();          
                        br.ReadInt32();                     
                        br.ReadInt64();                 
                        DSIG = br.ReadInt64();     
                        if(DSIG == -1)
                        { break;  }
                        else
                        {
                            br.BaseStream.Position = DSIG;
                            cab = DSIG;
                        }
                    }
                }
            }
            br.Close();             
        }

        private void eliminar(object sender, EventArgs e)
        {
            nAtributo = comboBox2.SelectedItem.ToString();
            dAtributo = buscaDireccionAtributo(nAtributo);

        }
        public long buscaDireccionAtributo(string nAtributo)
        {
            string n;
            long DSIG = 0;
            br = new BinaryReader(File.Open(archivo, FileMode.Open));
            br.BaseStream.Position = dEntidad; 
            br.ReadString();  
            br.ReadInt64();     
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                br.ReadChar();                    
                br.ReadInt32();                    
                dAtributo = br.ReadInt64();           
                br.ReadInt32();                     
                br.ReadInt64();                     
                DSIG = br.ReadInt64();               
                if(n == nAtributo)
                {
                    br.Close();
                    return dAtributo;
                }
                if (DSIG == -1)
                { break; }
                else { br.BaseStream.Position = DSIG; }
            }
            br.Close();
            return -1;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        { }
    }
}
