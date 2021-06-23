
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Linq;

namespace Diccionario_de_Datos
{
    public partial class Principal : Form
    {
        #region Variables Globales
        public string nArchivo;
        public long posicion;
        public long pSig;
        public int posArchivo = 0;
        List<string> datos = new List<string>();
        List<string> indi = new List<string>();
        private Arbol tree;
        public int TR;
        int valor = 0;
        List<string> secundarios = new List<string>();
        List<int> iSecundarios = new List<int>();
        ListaSecundario indiceSec;
        List<ListaSecundario> ListSec = new List<ListaSecundario>();
        long[,] indiceSecundario;
        int topeClave;
        int[] tope;
        int pos;
        int cont;
        int poSI = 0;
        private Arbol nodo;
        private Arbol arbol = new Arbol();
        private List<Arbol> Arbol = new List<Arbol>();
        int cont2 = 0;
        private int Col;
        private int Ren;
        private int posx;
        private int posy;
        private Entidad enti;
        private string _nombre;
        private char[] aux = new char[20];
        private List<Entidad> entidad;
        private long Cab, dir;
        private List<int> letras;
        private List<string> listaAtributos = new List<string>();
        private Atributo atri;
        private List<Atributo> atributo;
        private string nombreAtri;
        private int longi;
        private bool band, band2;
        private bool nuevo, abierto;
        BinaryWriter bw;
        BinaryReader br;
        BinaryWriter aBw;
        BinaryReader aBr;
        DataGridView grid;
        List<DataGridView> tablas = new List<DataGridView>();
        List<BinaryWriter> registro;
        List<BinaryWriter> indice;
        private int renglon = 0;
        private int contador;
        private int tipoIndice;
        List<string> reg = new List<string>();
        List<string> registros = new List<string>();
        List<indices> listaIndices = new List<indices>();
        List<indicep> claves;
        string nombre_indice;
        BinaryWriter fRegistro;
        BinaryWriter fIndice;
        bool nuevoAtributo;
        bool botonAtributo;
        string sEntidad;
        private indicep temp;
        private int tam;
        int[] claveSecundario;
        int iRen;
        int iCol;
        int iCol2;
        public int primero;
        public int TAMPRIM = 0;
        public int TAMSEC = 0;
        public struct indicep
        {
            public int clave;
            public long dir;
        }
        public struct indices
        {
            public int val;
            public int tipo;
        }
        #endregion

        #region Lista Registro
        List<Registro> lRegistro;
        #endregion

        #region Constructor
        public Principal()
        {
            InitializeComponent();
            posx = 0;
            posy = 0;
            entidad = new List<Entidad>();
            letras = new List<int>();
            Cab = -1;
            atri = new Atributo();
            atributo = new List<Atributo>();
            band = false;
            band2 = false;
            nuevo = false;
            abierto = false;
            registro = new List<BinaryWriter>();
            longi = 0;
            contador = 0;
            nuevoAtributo = false;
            botonAtributo = false;
            indice = new List<BinaryWriter>();
            tipoIndice = 0;
            tam = 0;
            primero = 0;
            lRegistro = new List<Registro>();
        }
        #endregion

        #region Algoritmos Entidad
        private void creaEntidad(object sender, EventArgs e)
        {
            enti = new Entidad();
            List<string> nombresOrdenados = new List<string>();
            long Cab = -1;
            long dir = -1;
            pSig = -1;
            int cont;
            _nombre = textBox1.Text;
            cont = _nombre.Length;
            for (; cont < 29; cont++)
            {
                _nombre += " ";
            }
            if (entidad.Count == 0)
            {
                enti.nombrate(_nombre);
                enti.direccionate(bw.BaseStream.Length);
                enti.ponteDireccionAtributo(-1);
                enti.ponteDireccionRegistro(-1);
                enti.ponteDireccionSig(-1);
                bw.Seek(0, SeekOrigin.Begin);
                bw.Write(enti.dameDE());
                cabecera.Text = enti.dameDE().ToString();
                Cab = enti.dameDE();
                bw.Write(enti.dameNombre());
                bw.Write(enti.dameDE());
                bw.Write(enti.dameDA());
                bw.Write(enti.dameDD());
                bw.Write(enti.dameDSIG());
            }
            else
            {
                enti.nombrate(_nombre);
                enti.direccionate(bw.BaseStream.Length);
                enti.ponteDireccionAtributo(-1);
                enti.ponteDireccionRegistro(-1);
                dir = buscaEntidad(enti.dameNombre());
                enti.ponteDireccionSig(dir);
                if (posicion == 1)
                {
                    bw.Seek(0, SeekOrigin.Begin);
                    cabecera.Text = enti.dameDE().ToString();
                    Cab = enti.dameDE();
                    bw.Write(Cab);
                }
                bw.Seek((int)bw.BaseStream.Length, SeekOrigin.Begin);
                bw.Write(enti.dameNombre());
                bw.Write(enti.dameDE());
                bw.Write(enti.dameDA());
                bw.Write(enti.dameDD());
                bw.Write(enti.dameDSIG());
            }
            entidad.Add(enti);
            imprimeLista(entidad);
        }
        public long buscaEntidad(string entidad)
        {
            long aux = -1;
            long TAM;
            long dirEntidad = 0;
            long sigEntidad = 0;
            long principio;
            string nEntidad;
            int compara;
            long antA = 0;
            long sigA;
            bw.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            TAM = br.BaseStream.Length;
            aux = br.ReadInt64();
            br.BaseStream.Position = aux;
            principio = aux;
            while (aux < TAM)
            {
                if (sigEntidad != -1)
                {
                    nEntidad = br.ReadString();
                    dirEntidad = br.ReadInt64();
                    br.ReadInt64();
                    br.ReadInt64();
                    sigEntidad = br.ReadInt64();
                    if (dirEntidad == aux)
                    {
                        compara = entidad.CompareTo(nEntidad);
                        switch (compara)
                        {
                            case -1:
                                br.Close();
                                bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
                                if (antA > 0)
                                {
                                    bw.BaseStream.Position = antA + 54;
                                    bw.Write(bw.BaseStream.Length);
                                }
                                bw.Close();
                                bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
                                if (principio == aux) { posicion = 1; }
                                else { posicion = -1; }
                                return dirEntidad;
                            case 0:
                                break;
                            case 1:
                                antA = dirEntidad;
                                sigA = sigEntidad;
                                if (sigEntidad == -1)
                                {
                                    br.Close();
                                    bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
                                    bw.BaseStream.Position = antA + 54;
                                    bw.Write(bw.BaseStream.Length);
                                    bw.BaseStream.Position = bw.BaseStream.Length;
                                    bw.Close();
                                    posicion = -1;
                                    bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
                                    return sigEntidad;
                                }
                                else
                                {
                                    br.BaseStream.Position = sigEntidad;
                                    aux = sigEntidad;
                                }
                                break;
                            default: break;
                        }
                    }
                    else
                    {
                        br.BaseStream.Position = sigEntidad;
                        aux = sigEntidad;
                    }
                }
            }
            return -1;
        }

        private void imprimeLista(List<Entidad> entidad)
        {
            comboBox1.Items.Clear();
            comboBox6.Items.Clear();
            dataGridView1.Rows.Clear();
            entidad.Clear();
            bw.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                enti = new Entidad();
                enti.nombrate(br.ReadString());
                enti.direccionate(br.ReadInt64());
                enti.ponteDireccionAtributo(br.ReadInt64());
                enti.ponteDireccionRegistro(br.ReadInt64());
                enti.ponteDireccionSig(br.ReadInt64());
                entidad.Add(enti);
                if (enti.dameDSIG() != -1) { br.BaseStream.Position = enti.dameDSIG(); }
                else
                    break;
            }
            br.Close();
            foreach (Entidad i in entidad)
            { dataGridView1.Rows.Add(i.dameNombre(), i.dameDE(), i.dameDA(), i.dameDD(), i.dameDSIG()); }
            foreach (Entidad i in entidad)
            { comboBox1.Items.Add(i.dameNombre()); }
            foreach (Entidad i in entidad)
            { comboBox6.Items.Add(i.dameNombre()); }
            bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
        }
        #endregion

        #region Algoritmos Atributo
        private void nombraAtributo(object sender, EventArgs e)
        { nombreAtri = textBox2.Text; }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        { }
        private void creaAtributo(object sender, EventArgs e)
        {
            atri = new Atributo();
            int cont;
            _nombre = textBox2.Text;
            cont = _nombre.Length;
            for (; cont <= 29; cont++)
            { _nombre += " "; }
            if (atributo.Count == 0)
            {
                atri.nombrate(_nombre);
                atri.ponteTipo(Convert.ToChar(comboTipo.SelectedItem));
                atri.ponteLongitud(Convert.ToInt32(textBox3.Text));
                atri.direccionate((long)bw.BaseStream.Length);
                atri.ponteTipoIndice((int)tipoIndice);
                atri.ponteDirIndice((long)-1);
                atri.ponteDirSig((long)-1);
                bw.Seek((int)bw.BaseStream.Length, SeekOrigin.Begin);
                bw.Write(atri.dameNombre());
                bw.Write(atri.dameTipo());
                bw.Write(atri.dameLongitud());
                bw.Write(atri.dameDireccion());
                bw.Write(atri.dameTI());
                bw.Write(atri.dameDirIndice());
                bw.Write(atri.dameDirSig());
            }
            else
            {
                atri.nombrate(_nombre);
                atri.ponteTipo(Convert.ToChar(comboTipo.SelectedItem));
                atri.ponteLongitud(Convert.ToInt32(textBox3.Text));
                atri.direccionate((long)bw.BaseStream.Length);
                atri.ponteTipoIndice((int)tipoIndice);
                atri.ponteDirIndice((long)-1);
                atri.ponteDirSig((long)-1);
                bw.BaseStream.Position = bw.BaseStream.Length;
                bw.Write(atri.dameNombre());
                bw.Write(atri.dameTipo());
                bw.Write(atri.dameLongitud());
                bw.Write(atri.dameDireccion());
                bw.Write(atri.dameTI());
                bw.Write(atri.dameDirIndice());
                bw.Write(atri.dameDirSig());
            }
            atributo.Add(atri);
            actualizaEntidad(atri.dameDireccion());
            imprimeLista(entidad);
            imprimeAtributo(atributo);
        }
        public void actualizaEntidad(long dAtributo)
        {
            string nEntidad = comboBox1.Text;
            string n = " ";
            long DSIG = 0;
            long DSIGA = 0;
            long DA = 0;
            long pos = 0;
            bw.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                if (DSIG != -1)
                {
                    n = br.ReadString();
                    br.ReadInt64();
                    pos = br.BaseStream.Position;
                    DA = br.ReadInt64();
                    br.ReadInt64();
                    DSIG = br.ReadInt64();
                    if (DSIG != -1)
                        br.BaseStream.Position = DSIG;
                }
                if (n == nEntidad)
                {
                    if (DA != -1)
                    {
                        br.BaseStream.Position = DA;
                        while (DSIGA != -1)
                        {
                            br.ReadString();
                            br.ReadChar();
                            br.ReadInt32();
                            br.ReadInt64();
                            br.ReadInt32();
                            br.ReadInt64();
                            pos = br.BaseStream.Position;
                            DSIGA = br.ReadInt64();
                            if (DSIGA == -1)
                            {
                                br.Close();
                                bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
                                bw.BaseStream.Position = pos;
                                bw.Write(dAtributo);
                                bw.Close();
                                br.Close();
                                br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
                                DSIGA = -1;
                            }
                            else
                                br.BaseStream.Position = DSIGA;
                        }
                        break;
                    }
                    else
                    {
                        br.Close();
                        bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
                        bw.BaseStream.Position = pos;
                        bw.Write(dAtributo);
                        bw.Close();
                        br.Close();
                        br.Close();
                        br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
                        br.BaseStream.Position = br.BaseStream.Length;
                        break;
                    }
                }
            }
            br.Close();
            bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
        }

        private void seleccionaEntidad(object sender, EventArgs e)
        {
            contador = 0;
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            dataGridView4.Columns.Clear();
            renglon = 0;
            dataGridView4.Columns.Add("Dirección del Registro", "Dirección del Registro");
            nuevoAtributo = true;
            Atributo iArbol = dameIndiceArbol(comboBox6.Text);
            if (iArbol != null)
            {
                if (tree.Count > 0)
                {
                    if (tree.dameRaiz() != -1)
                    { actualizaRaiz(tree.dameRaiz(), iArbol); }
                    else { actualizaRaiz(tree[0].dirNodo, iArbol); }
                }
                else
                    actualizaRaiz(Convert.ToInt64(-1), iArbol);
            }
            imprimeAtributo(atributo);
        }

        private void imprimeAtributo(List<Atributo> atributo)
        {
            string n = comboBox1.Text;
            string nEntidad = "";
            long DA = 0;
            long DSIG = 0;
            bw.Close();
            br.Close();
            atributo.Clear();
            dataGridView2.Rows.Clear();
            br.Close();
            br.Close();
            br.Close();
            br.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                nEntidad = br.ReadString();
                br.ReadInt64();
                DA = br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (nEntidad == n)
                {
                    if (DA != -1)
                    {
                        br.BaseStream.Position = DA;
                        while (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            atri = new Atributo();
                            atri.nombrate(br.ReadString());
                            atri.ponteTipo(br.ReadChar());
                            atri.ponteLongitud(br.ReadInt32());
                            atri.direccionate(br.ReadInt64());
                            atri.ponteTipoIndice(br.ReadInt32());
                            atri.ponteDirIndice(br.ReadInt64());
                            atri.ponteDirSig(br.ReadInt64());
                            atributo.Add(atri);
                            if (atri.dameDirSig() != -1)
                                br.BaseStream.Position = atri.dameDirSig();
                            else
                                break;
                        }
                    }
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }

            br.Close();
            bw.Close();
            bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
            foreach (Atributo a in atributo)
            {
                dataGridView2.Rows.Add(a.dameNombre(), a.dameTipo(), a.dameLongitud(), a.dameDireccion(), a.dameTI(), a.dameDirIndice(), a.dameDirSig());

            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            bw.Close();
            long dAtributo, dEntidad, dir, dSig, anterior;
            string nEntidad, nAtributo;
            string n;
            List<string> entidades = new List<string>();
            foreach (Entidad i in entidad)
            {
                entidades.Add(i.dameNombre());
            }
            EliminaAtributo eliminaAtributo = new EliminaAtributo(entidades, nArchivo);
            if (eliminaAtributo.ShowDialog() == DialogResult.OK)
            {
                dAtributo = eliminaAtributo.dAtributo;
                nAtributo = eliminaAtributo.nAtributo;
                nEntidad = eliminaAtributo.nEntidad;

                br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
                br.BaseStream.Position = br.ReadInt64();
                dEntidad = buscaDirEntidad(nEntidad);
                br.BaseStream.Position = dEntidad;
                br.ReadString();
                br.ReadInt64();
                dir = br.ReadInt64();
                br.ReadInt64();
                dSig = br.ReadInt64();
                if (dir == dAtributo)
                {
                    br.BaseStream.Position = dir;
                    br.ReadString();
                    br.ReadChar();
                    br.ReadInt32();
                    br.ReadInt64();
                    br.ReadInt32();
                    br.ReadInt64();
                    dSig = br.ReadInt64();
                    if (dSig == -1)
                    {
                        eliminaPrimero(dEntidad);
                        bw.BaseStream.Position = dAtributo;
                        string temp = "NULL";
                        while (temp.Length <= 29)
                            temp += " ";
                        bw.Write(temp);

                    }
                    else
                    {
                        br.Close();
                        bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
                        bw.BaseStream.Position = dAtributo;
                        string temp = "NULL";
                        while (temp.Length <= 29)
                            temp += " ";
                        bw.Write(temp);
                        bw.BaseStream.Position = dEntidad + 38;
                        bw.Write(dSig);
                    }

                }
                else
                {
                    br.Close();
                    bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
                    bw.BaseStream.Position = dAtributo;
                    string temp = "NULL";
                    while (temp.Length <= 29)
                        temp += " ";
                    bw.Write(temp);
                    dSig = dAtributo + 55;
                    anterior = obtenAtributoAnterior(dAtributo, dEntidad);
                    bw.BaseStream.Position = anterior + 55;
                    dSig = obtenAtributoSiguiente(dSig);
                    bw.BaseStream.Position = anterior + 55;
                    bw.Write(dSig);
                    imprimeLista(entidad);
                    imprimeAtributo(atributo);
                }
            }
            imprimeLista(entidad);
            imprimeAtributo(atributo);
        }
        #region MetodosELiminaAtributo
        public void eliminaPrimero(long dir)
        {
            br.Close();
            bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
            bw.BaseStream.Position = dir + 38; //nombre + dir + atributo + dd + sig

            bw.Write((long)-1);
            //bw.Close();
            //br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            imprimeLista(entidad);
            nuevoAtributo = true;
        }
        /*****************************************************************************
         * Metodo encargado de recorrer la lista de atributos y obtener la dirección
         * del atributo que apunta al que se va a eliminar
         *****************************************************************************/
        public long obtenAtributoAnterior(long dAtributo, long dEntidad)
        {
            long dir, DSIG = 0;
            int totalEntidad;
            string n;
            bw.Close();

            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = dEntidad; // posiciona en la direccion de la entidad
            n = br.ReadString();
            br.ReadInt64();
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                br.ReadChar();
                br.ReadInt32();
                dir = br.ReadInt64();
                br.ReadInt32();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (DSIG != -1)
                {
                    br.BaseStream.Position = DSIG;
                }
                if (DSIG == dAtributo)
                {
                    br.Close();
                    bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
                    return dir;
                }
                else
                    break;

            }
            br.Close();
            bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
            return -1;
        }
        public long obtenAtributoSiguiente(long dSig)
        {
            long dir = -1;
            bw.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = dSig;
            dir = br.ReadInt64();

            br.Close();
            bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
            return dir;
        }
        #endregion
        public long buscaDirEntidad(string nEntidad)
        {
            long pos;
            string n;
            long dEntidad;
            long DSIG = 0;
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            br.BaseStream.Position = br.ReadInt64();
            pos = br.BaseStream.Position;
            while (DSIG != -1)
            {
                n = br.ReadString();
                dEntidad = br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == nEntidad)
                {
                    return dEntidad;
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;

            }
            return -1;
        }

        #endregion

        #region Apertura del Archivo
        /*****************************************************************************
         * 
         * Metodo que permite abrir el archivo y lo envia a uno nuevo
         * 
         *****************************************************************************/
        private void abreArchivo(object sender, EventArgs e)
        {
            string nombre = " ";
            string n;
            int totalEntidad = 0;
            long apuntadorAtributo = 0;
            long pos = 0;
            long DSIG = 0;
            button1.Enabled = true;
            modificar.Enabled = true;
            button3.Enabled = true;
            textBox1.Enabled = true;
            Archivo archivo = new Archivo(nombre);
            if (nuevo)
            {
                bw.Close();
            }

            if (archivo.ShowDialog() == DialogResult.OK)
            {
                nombre = archivo.nombre + ".bin";
                nArchivo = nombre;
                if (File.Exists(nombre))
                {
                    br = new BinaryReader(File.Open(nombre, FileMode.Open));
                    nuevo = true;
                    button7.Hide(); // Se oculta boton Nuevo
                    button8.Hide(); // Se oculta boton Abrir
                    /********************************************/

                    // 1. Se lee la cabecera del Archivo
                    pos = br.ReadInt64();
                    cabecera.Text = pos.ToString(); // Se muestra en el textbox

                    // 2. Se posiciona el apuntador 
                    br.BaseStream.Position = pos;
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        br.ReadString();
                        br.ReadInt64();
                        br.ReadInt64();
                        br.ReadInt64();
                        DSIG = br.ReadInt64();
                        totalEntidad++;
                        if (DSIG == -1)
                        {
                            break;
                        }
                        br.BaseStream.Position = DSIG;
                    }
                    // MessageBox.Show("Total de entidades: " + totalEntidad);
                    br.BaseStream.Position = 8;
                    for (int i = 0; i < totalEntidad; i++)
                    {
                        enti = new Entidad();
                        //  MessageBox.Show("Posición " + br.BaseStream.Position);
                        enti.nombrate(br.ReadString());
                        if (enti.dameNombre() == "NULL                          ")
                        {
                            br.BaseStream.Position = br.BaseStream.Position + 31;
                            enti.nombrate(br.ReadString());
                        }
                        comboBox1.Items.Add(enti.dameNombre());
                        comboBox6.Items.Add(enti.dameNombre());
                        enti.direccionate(br.ReadInt64());
                        enti.ponteDireccionAtributo(br.ReadInt64());
                        enti.ponteDireccionRegistro(br.ReadInt64());
                        enti.ponteDireccionSig(br.ReadInt64());
                        entidad.Add(enti);
                    }

                    /********************************************/

                    br.Close();
                    bw = new BinaryWriter(File.Open(nombre, FileMode.Open));
                    imprimeLista(entidad);
                    imprimeAtributo(atributo);

                }
                else
                {
                    MessageBox.Show("El proyecto no existe!");
                }

            }


            /*

            button8.Hide();
            long tam;
            long sig;
            
            
            br = new BinaryReader(File.Open("Entidad.bin", FileMode.Open));
            button7.Hide();
            string swap;
            
            tam = 0;
            cabecera.Text = br.ReadInt64().ToString();
            while (tam < br.BaseStream.Length)
            {
                MessageBox.Show("apuntador : " + tam);
                enti = new Entidad();
                enti.nombrate(br.ReadString());
                comboBox1.Items.Add(enti.dameNombre());
                enti.direccionate(br.ReadInt64());
                enti.ponteDireccionAtributo(br.ReadInt64());
                enti.ponteDireccionRegistro(br.ReadInt64());
                sig = br.ReadInt64();
                if (sig == -2)
                {

                    enti.ponteDireccionSig(-2);
                    entidad.Add(enti);
                    break;
                  
                }
                else
                {
                    enti.ponteDireccionSig(sig);
                    entidad.Add(enti);
                }
                
                
                tam = br.BaseStream.Position;
               // break;
                // if (sig == -1 && tam < br.BaseStream.Length)
                // {
                //   enti.ponteDireccionSig(sig);
                //    entidad.Add(enti);
                //     imprimeLista(entidad);
                //     abreAtributos(br, tam);
                //    break;

                // }
                //  else
                //  {

                //  }



            }
            imprimeLista(entidad);

            br.Close();
            bw = new BinaryWriter(File.Open("Entidad.bin", FileMode.Open));
            nuevo = true;
            */

        }

        #endregion

        #region Codigo
        private void tabPage1_Click(object sender, EventArgs e)
        { }
        private void insertarRegistro_Click(object sender, EventArgs e)
        {
            br.Close();
            bw.Close();
            char tipo = ' ';
            int tamC = 0;
            List<int> pos2 = new List<int>();
            string dat = comboBox6.Text + ".dat";
            string indi = comboBox6.Text + ".idx";
            string celda = "";
            Atributo aClave;
            int pos = 0;
            int posprimario = 0;
            int posArbol = 0;
            Atributo iPrimario;
            Atributo iSecundario;
            Atributo iArbol;
            string nRegistro = " ";
            long DR = 0, DSIGR = 0;
            br.Close();
            bw.Close();
            aClave = dameClaveBusqueda(dat);
            iPrimario = dameIndicePrimario(comboBox6.Text);
            iSecundario = dameIndiceSecundario(comboBox6.Text);
            iArbol = dameIndiceArbol(comboBox6.Text);
            secundarios = buscaSecundarios(comboBox6.Text);
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                if (aClave != null)
                {
                    if (dataGridView3.Columns[i].Name == aClave.dameNombre())
                    {
                        pos = i;
                        break;
                    }
                }
            }
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                if (iPrimario != null)
                {
                    if (dataGridView3.Columns[i].Name == iPrimario.dameNombre())
                    {
                        posprimario = i;
                        break;
                    }
                }
            }
            int j = 0;
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                if (iSecundario != null)
                {
                    if (j < secundarios.Count)
                        if (dataGridView3.Columns[i].Name == secundarios[j])
                        {
                            pos2.Add(i);
                            j++;
                        }
                }
            }
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                if (iArbol != null)
                {
                    if (dataGridView3.Columns[i].Name == iArbol.dameNombre())
                    {
                        posArbol = i;
                        break;
                    }
                }
            }
            foreach (string arch in datos)
            {
                if (arch == dat)
                {
                    bw = new BinaryWriter(File.Open(dat, FileMode.Open));
                    break;
                }
            }
            bw.BaseStream.Position = bw.BaseStream.Length;
            DR = bw.BaseStream.Length;
            bw.Write(DR);
            primero = 8;
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                bw.Close();
                br.Close();
                tipo = buscaTipo(comboBox6.Text, dataGridView3.Columns[i].Name);
                bw.Close();
                br.Close();
                celda = dataGridView3.Rows[0].Cells[i].Value.ToString();
                bw.Close();
                br.Close();
                bw = new BinaryWriter(File.Open(dat, FileMode.Open));
                if (tipo == 'E')
                {
                    bw.BaseStream.Position = bw.BaseStream.Length;
                    bw.Write(Convert.ToInt32(celda));
                    primero += 4;
                }
                else if (tipo == 'C')
                {
                    tamC = buscaTam(comboBox6.Text, dataGridView3.Columns[i].Name);
                    while (celda.Length < tamC - 1)
                    {
                        celda += " ";
                    }
                    if (pos == i)
                    {
                        nRegistro = celda;
                    }
                    bw.Close();
                    br.Close();
                    bw = new BinaryWriter(File.Open(dat, FileMode.Open));
                    bw.BaseStream.Position = bw.BaseStream.Length;
                    bw.Write(celda);
                    primero += celda.Length + 1;
                }
            }
            primero += 8;
            bw.Write((long)-1);
            if (primero < bw.BaseStream.Length)
            {
                bw.Close();
                br.Close();
                if (aClave != null)
                {
                    bw.Close();
                    br.Close();
                    if (aClave.dameTipo() == 'C')
                    {
                        bw.Close();
                        br.Close();
                        DSIGR = siguienteRegistro(dat, aClave, primero, comboBox6.Text, nRegistro, DR);
                        br.Close();
                        bw.Close();
                        bw.Close();
                        bw.Close();
                        bw.Close();
                        bw.Close();
                        bw.Close();
                        bw = new BinaryWriter(File.Open(dat, FileMode.Open));
                        bw.BaseStream.Position = bw.BaseStream.Length - 8;
                        bw.Write(DSIGR);
                    }
                    else if (aClave.dameTipo() == 'E')
                    { }
                }
                else
                {
                    bw.Close();
                    bw.Close();
                    bw.Close();
                    bw.Close();
                    bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                    bw.BaseStream.Position = bw.BaseStream.Length - 8 - primero;
                    DSIGR = DR;
                    bw.Write(DSIGR);
                }
            }
            else if (primero == bw.BaseStream.Length)
            { ponteRegistro(comboBox6.Text, DR); }
            if (dameDireccionRegistro(comboBox6.Text) == -1)
            { ponteRegistro(comboBox6.Text, DR); }
            bw.Close();
            br.Close();
            imprimeRegistro(dat);
            br.Close();
            bw.Close();
            if (iPrimario != null)
            {
                bw.Close();
                br.Close();
                insertaPrimario(indi, iPrimario, posprimario);
            }
            if (iArbol != null)
            {
                br.Close();
                bw.Close();
                insertaNodo(comboBox6.Text + ".idx", iArbol, posArbol);
                br.Close();
                bw.Close();
            }
            bw.Close();
            br.Close();
        }
        public List<string> buscaSecundarios(string nEntidad)
        {
            string n = " ";
            long DSIG, DA, DSIGA;
            string cSEC;
            int tipo;
            br.Close();
            bw.Close();
            List<string> sec = new List<string>();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                br.ReadInt64();
                DA = br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == nEntidad)
                {
                    br.BaseStream.Position = DA;
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        cSEC = br.ReadString();
                        br.ReadChar();
                        br.ReadInt32();
                        br.ReadInt64();
                        tipo = br.ReadInt32();
                        br.ReadInt64();
                        DSIGA = br.ReadInt64();
                        if (tipo == 3)
                            sec.Add(cSEC);
                        if (DSIGA != -1)
                            br.BaseStream.Position = DSIGA;
                        else
                            break;
                    }
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            br.Close();
            return sec;
        }
        private void actualizaRaiz(long raiz, Atributo iArbol)
        {
            bw.Close();
            bw.Close();
            bw.Close();
            bw.Close();
            bw.Close();
            bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
            bw.BaseStream.Position = iArbol.dameDireccion() + 30 + 1 + 4 + 8 + 5;
            bw.Write(Convert.ToInt64(raiz));
            bw.Close();
            br.Close();
        }
        private void insertaNodo(string arch, Atributo iArbol, int posArbol)
        {
            br.Close();
            bw.Close();
            bool lleno = false;
            bool existeIntermedio = false;
            List<int> nodos = new List<int>();
            List<int> direcciones = new List<int>();
            List<int> tempClaves;
            List<long> tempDir;
            for (int i = 0; i < dataGridView4.Rows.Count - 2; i++)
            {
                nodos.Add(Convert.ToInt32(dataGridView4.Rows[i].Cells[posArbol + 1].Value));
                direcciones.Add(Convert.ToInt32(dataGridView4.Rows[i].Cells[0].Value));
            }
            actualizaRaiz(0, iArbol);
            Arbol arbol = new Arbol();
            Nodo anterior = new Nodo();
            if (direcciones[0] == 0)
                direcciones[0] = 1;
            Nodo nuevo;
            long dirRaiz = 0;
            nuevo = new Nodo();
            for (int i = 0; i < nodos.Count; i++)
            {
                dirRaiz = arbol.dameRaiz();
                if (dirRaiz == -1)
                {
                    if (lleno == false)
                    {
                        if (arbol.Count == 0)
                        {

                            nuevo = arbol.creaNodo(nuevo, dameTamArchivo());
                            arbol.Add(nuevo);
                            lleno = arbol.agregaDato(nuevo.dirNodo, nodos[i], direcciones[i]);
                        }
                        else
                        {
                            lleno = arbol.agregaDato(nuevo.dirNodo, nodos[i], direcciones[i]);
                            anterior = nuevo;
                        }
                    }
                    if (lleno == true)
                    {
                        nuevo = new Nodo();
                        nuevo = arbol.creaNodo(nuevo, arbol[arbol.Count - 1].dirNodo + 65);
                        anterior.sig = nuevo.dirNodo;
                        arbol.Add(nuevo);
                        tempClaves = new List<int>();
                        tempDir = new List<long>();
                        for (int aux = 2; aux < anterior.clave.Count; aux++)
                        {
                            tempClaves.Add(anterior.clave[aux]);
                            tempDir.Add(anterior.direccion[aux]);
                        }
                        anterior.clave.RemoveAt(3);
                        anterior.clave.RemoveAt(2);
                        anterior.direccion.RemoveAt(3);
                        anterior.direccion.RemoveAt(2);
                        tempClaves.Add(nodos[i]);
                        tempDir.Add(direcciones[i]);
                        arbol.ordenaCinco(tempClaves, tempDir);
                        for (int aux = 0; aux < tempClaves.Count; aux++)
                        {
                            lleno = arbol.agregaDato(nuevo.dirNodo, tempClaves[aux], tempDir[aux]);
                        }
                        anterior = nuevo;
                        nuevo = new Nodo();
                        nuevo = arbol.creaNodo(nuevo, anterior.dirNodo + 65);
                        nuevo.tipo = 'R';
                        nuevo.direccion.Add(arbol[arbol.Count - 2].dirNodo);
                        nuevo.clave.Add(tempClaves[0]);
                        nuevo.direccion.Add(arbol[arbol.Count - 1].dirNodo);
                        arbol.Add(nuevo);
                    }
                    actualizaRaiz(arbol[0].dirNodo, iArbol);

                }
                else
                {
                    actualizaRaiz(arbol.dameRaiz(), iArbol);
                    long dir = arbol.buscaNodo(nodos[i]);
                    long raiz = arbol.dameRaiz();
                    Nodo actual = arbol.dameNodo(dir);
                    lleno = arbol.agregaDato(dir, nodos[i], direcciones[i]);
                    if (lleno == true)
                    {
                        long dirAnt;
                        dirAnt = actual.dirNodo;
                        nuevo = new Nodo();
                        nuevo = arbol.creaNodo(nuevo, arbol[arbol.Count - 1].dirNodo + 65);
                        nuevo.sig = actual.sig;
                        actual.sig = nuevo.dirNodo;
                        arbol.Add(nuevo);
                        tempClaves = new List<int>();
                        tempDir = new List<long>();
                        for (int aux = 0; aux < actual.clave.Count; aux++)
                        {
                            tempClaves.Add(actual.clave[aux]);
                            tempDir.Add(actual.direccion[aux]);
                        }
                        tempClaves.Add(nodos[i]);
                        tempDir.Add(direcciones[i]);
                        arbol.ordenaCinco(tempClaves, tempDir);
                        actual.clave.Clear();
                        actual.direccion.Clear();
                        arbol.agregaDato(actual.dirNodo, tempClaves[0], tempDir[0]);
                        arbol.agregaDato(actual.dirNodo, tempClaves[1], tempDir[1]);
                        for (int aux = 2; aux < tempClaves.Count; aux++)
                        {
                            lleno = arbol.agregaDato(nuevo.dirNodo, tempClaves[aux], tempDir[aux]);
                        }
                        actualizaRaiz(arbol.dameRaiz(), iArbol);
                        existeIntermedio = arbol.existeIntermedio();
                        if (existeIntermedio == false)
                        {
                            lleno = arbol.agregaDato(raiz, tempClaves[2], arbol[arbol.Count - 1].dirNodo);
                            if (arbol.dameNodo(arbol.dameRaiz()).clave.Count == 4)
                            {
                                Nodo rtemp = new Nodo();
                                rtemp = arbol.dameNodo(arbol.dameRaiz());
                                rtemp.sig = rtemp.direccion[4];
                            }
                            arbol.ordenaRaiz();
                            if (lleno == true)
                            {
                                raiz = arbol.dameRaiz();
                                Nodo nRaiz = arbol.dameNodo(raiz);
                                int nDato = tempClaves[2];
                                long dDato = arbol[arbol.Count - 1].dirNodo;
                                tempClaves = new List<int>();
                                tempDir = new List<long>();
                                foreach (int n in nRaiz.clave)
                                { tempClaves.Add(n); }
                                foreach (long l in nRaiz.direccion)
                                { tempDir.Add(l); }
                                tempClaves.Add(nDato);
                                tempDir.Add(dDato);
                                arbol.ordenaCinco(tempClaves, tempDir);
                                Nodo intermedio = new Nodo();
                                intermedio = arbol.creaNodo(intermedio, arbol[arbol.Count - 1].dirNodo + 65);
                                intermedio.tipo = 'I';
                                nRaiz.tipo = 'I';
                                nRaiz.sig = intermedio.dirNodo;
                                arbol.Add(intermedio);
                                intermedio.clave.Add(tempClaves[3]);
                                intermedio.clave.Add(tempClaves[4]);
                                intermedio.direccion.Add(tempDir[3]);
                                intermedio.direccion.Add(tempDir[4]);
                                intermedio.direccion.Add(tempDir[5]);
                                arbol.ordenaIntermedio(intermedio.dirNodo);
                                nRaiz.clave.RemoveAt(3);
                                nRaiz.clave.RemoveAt(2);
                                nRaiz.direccion.RemoveAt(4);
                                nRaiz.direccion.RemoveAt(3);
                                Nodo Cab = new Nodo();
                                Cab = arbol.creaNodo(Cab, arbol[arbol.Count - 1].dirNodo + 65);
                                Cab.tipo = 'R';
                                Cab.direccion.Add(nRaiz.dirNodo);
                                Cab.clave.Add(tempClaves[2]);
                                Cab.direccion.Add(intermedio.dirNodo);
                                arbol.Add(Cab);
                                actualizaRaiz(arbol.dameRaiz(), iArbol);
                            }
                        }
                        if (existeIntermedio == true)
                        {
                            long dirIntermedio = arbol.dameIntermedio(nuevo.clave[0]);
                            Nodo inter;
                            int temp;
                            long temp2;
                            Nodo este = arbol.dameNodo(dirIntermedio);
                            lleno = arbol.agregaDato(dirIntermedio, tempClaves[0], arbol[arbol.Count - 1].dirNodo);
                            temp = este.clave[0];
                            este.clave[0] = este.clave[1];
                            este.clave[1] = temp;
                            temp2 = este.direccion[1];
                            este.direccion[1] = este.direccion[2];
                            este.direccion[2] = temp2;
                            arbol.ordenaIntermedio(este.dirNodo);
                            if (este.clave.Count == 4)
                            { este.sig = este.direccion[4]; }
                            if (lleno == true)
                            {
                                int nDato = este.clave[2];
                                long dDato = este.dirNodo;
                                raiz = arbol.dameRaiz();
                                Nodo nRaiz = arbol.dameNodo(raiz);
                                tempClaves = new List<int>();
                                tempDir = new List<long>();
                                foreach (int c in este.clave)
                                    tempClaves.Add(c);
                                foreach (long l in este.direccion)
                                    tempDir.Add(l);
                                tempDir.Add(dDato);
                                arbol.ordenaCinco(tempClaves, tempDir);
                                inter = arbol.dameNodo(dirIntermedio);
                                Nodo intermedio = new Nodo();
                                intermedio = arbol.creaNodo(intermedio, arbol[arbol.Count - 1].dirNodo + 65);
                                intermedio.tipo = 'I';
                                inter.sig = intermedio.dirNodo;
                                arbol.Add(intermedio);
                                intermedio.clave.Add(tempClaves[3]);
                                intermedio.clave.Add(nuevo.clave[0]);
                                intermedio.direccion.Add(tempDir[5]);
                                intermedio.direccion.Add(tempDir[4]);
                                intermedio.direccion.Add(nuevo.dirNodo);
                                arbol.ordenaIntermedio(intermedio.dirNodo);
                                inter.clave.RemoveAt(3);
                                inter.clave.RemoveAt(2);
                                inter.direccion.RemoveAt(4);
                                inter.direccion.RemoveAt(3);
                                long dR = arbol.dameRaiz();
                                Nodo nR = arbol.dameNodo(dR);
                                if (nR.clave.Count < 4)
                                {
                                    nR.clave.Add(tempClaves[2]);
                                    nR.direccion.Add(intermedio.dirNodo);
                                    arbol.ordenaRaiz();
                                }
                            }
                        }
                        actualizaRaiz(arbol.dameRaiz(), iArbol);
                    }
                }
            }
            tree = arbol;
            if (arbol.dameRaiz() == -1)
                actualizaRaiz(arbol[0].dirNodo, iArbol);
            else
                actualizaRaiz(arbol.dameRaiz(), iArbol);
            imprimeArbol(arbol);
            guardaArbol();
        }
        public void guardaArbol()
        {
            bw.Close();
            br.Close();
            Atributo iSecundario = dameIndiceSecundario(comboBox6.Text);
            bw = new BinaryWriter(File.Open(comboBox6.Text + ".idx", FileMode.Create));
            if (iSecundario != null)
                imprimeSecundario(comboBox6.Text);
            bw.Close();
            bw = new BinaryWriter(File.Open(comboBox6.Text + ".idx", FileMode.Open));
            bw.BaseStream.Position = TAMSEC;
            for (int i = 0; i < tablaArbol.Rows.Count - 1; i++)
            {
                for (int j = 0; j < tablaArbol.Columns.Count; j++)
                {
                    if (j == 0)
                    { bw.Write(Convert.ToInt64(tablaArbol.Rows[i].Cells[j].Value)); }
                    else if (j == 1)
                    { bw.Write(Convert.ToChar(tablaArbol.Rows[i].Cells[j].Value)); }
                    else if (j % 2 == 0)
                    { bw.Write(Convert.ToInt64(tablaArbol.Rows[i].Cells[j].Value)); }
                    else if (j % 2 != 0)
                    { bw.Write(Convert.ToInt32(tablaArbol.Rows[i].Cells[j].Value)); }
                }
            }
            bw.Close();
            br.Close();
        }
        private void imprimeArbol(Arbol arbol)
        {
            int k = 0;
            int i = 0;
            tablaArbol.Rows.Clear();
            if (arbol != null)
            {
                foreach (Nodo nodo in arbol)
                {
                    if (nodo.dirNodo != -1)
                    {
                        k = 0;
                        tablaArbol.Rows.Add();
                        for (int j = 0; j < tablaArbol.Columns.Count; j++)
                        {
                            if (j == 0)
                            { tablaArbol.Rows[i].Cells[j].Value = nodo.dirNodo; }
                            if (j == 1)
                            { tablaArbol.Rows[i].Cells[j].Value = nodo.tipo; }
                            if (j > 1 && j < tablaArbol.Columns.Count - 1)
                            {
                                if (k < nodo.direccion.Count)
                                    tablaArbol.Rows[i].Cells[j].Value = nodo.direccion[k];
                                j++;
                                if (k < nodo.clave.Count)
                                    tablaArbol.Rows[i].Cells[j].Value = nodo.clave[k];
                                k++;
                            }
                            if (j == tablaArbol.Columns.Count - 1)
                                tablaArbol.Rows[i].Cells[j].Value = nodo.sig;
                        }
                        i++;
                    }
                }
                tree = arbol;
            }
        }
        private void insertaSecundario(string indi, List<int> iSecundarios)
        {
            int posSecundario = 0;
            br.Close();
            bw.Close();
            Secundario s;
            Atributo iSecundario = new Atributo();
            Atributo iPrimario = dameIndicePrimario(comboBox6.Text);
            int pos = 0;
            poSI = 0;
            int j = 0;
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                if (j < secundarios.Count)
                    if (dataGridView3.Columns[i].Name == secundarios[j])
                    {
                        iSecundarios.Add(i);
                        if (secundarios[j] == comboSecundario.Text)
                        {
                            poSI = TAMSEC * j;
                            iSecundario = dameIndiceSecundario(comboBox6.Text, secundarios[j]);
                            bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
                            if (iPrimario != null)
                                TAMPRIM = (4 + 8) * 20;
                            else
                                TAMPRIM = 0;
                            bw.BaseStream.Position = iSecundario.dameDireccion() + 30 + 1 + 4 + 8 + 5;
                            if (j == 0)
                                bw.Write(Convert.ToInt64(TAMPRIM));
                            else
                                bw.Write(Convert.ToUInt64(TAMSEC * j));
                            bw.Close();
                        }
                        j++;
                    }
            }
            indiceSec = new ListaSecundario();
            indiceSec.nombre = comboSecundario.Text;
            indiceSec.posTabla = pos;
            indiceSec.posTabla = posSecundario;
            List<List<Secundario>> listaSecundario = new List<List<Secundario>>();
            List<Secundario> secundario = new List<Secundario>();
            j = 0;
            foreach (string nn in secundarios)
            {
                if (comboSecundario.Text == nn)
                {
                    posSecundario = iSecundarios[j];
                }
                j++;
            }
            for (int i = 0; i < dataGridView4.Rows.Count - 2; i++)
            {
                s = new Secundario();
                if (i == 0)
                {
                    s.clave = Convert.ToString(dataGridView4.Rows[i].Cells[posSecundario + 1].Value);
                    s.direccion.Add(Convert.ToInt64(dataGridView4.Rows[i].Cells[0].Value));
                    secundario.Add(s);
                }
                else
                {
                    s.clave = Convert.ToString(dataGridView4.Rows[i].Cells[posSecundario + 1].Value);
                    Secundario existe = secundario.Find(x => x.clave.Equals(s.clave));
                    if (existe != null)
                    {
                        foreach (Secundario aux in secundario)
                        {
                            if (aux.clave == s.clave)
                            { aux.direccion.Add(Convert.ToInt64(dataGridView4.Rows[i].Cells[0].Value)); }
                        }
                    }
                    else
                    {
                        s.direccion.Add(Convert.ToInt64(dataGridView4.Rows[i].Cells[0].Value));
                        secundario.Add(s);
                    }
                }
            }
            secundario = secundario.OrderBy(x => x.clave).ToList();
            foreach (Secundario ss in secundario)
            {
                ss.direccion.OrderBy(x => x).ToList();
            }
            listaSecundario.Add(secundario);
            bw.Close();
            br.Close();
            bw = new BinaryWriter(File.Open(indi + ".idx", FileMode.Open));
            bw.BaseStream.Position = poSI;
            foreach (Secundario sec in secundario)
            {
                bw.Write(sec.clave);
                foreach (long p in sec.direccion)
                {
                    bw.Write(p);
                }
            }
            bw.Close();
            int u = 0;
            int t = 0;
            foreach (Secundario sec in secundario)
            {
                tablaSecundario.Rows.Add();
                tablaSecundario.Rows[u].Cells[0].Value = sec.clave;
                t = 1;
                foreach (long p in sec.direccion)
                {
                    if (t >= tablaSecundario.Columns.Count)
                    { tablaSecundario.Columns.Add("direccion", "direccion"); }
                    tablaSecundario.Rows[u].Cells[t].Value = p;
                    t++;
                }
                u++;
            }
        }
        public long dameTamArchivo()
        {
            Atributo iSecundario = dameIndiceSecundario(comboBox6.Text);
            long dir = 0;
            bw.Close();
            br.Close();
            br = new BinaryReader(File.Open(comboBox6.Text + ".idx", FileMode.Open));
            dir = 0;
            if (iSecundario != null)
                dir = TAMSEC;
            bw.Close();
            br.Close();
            return dir;

        }
        public void imprimeSecundario(string indi)
        {
            br.Close();
            bw.Close();
            br = new BinaryReader(File.Open(indi += ".idx", FileMode.Open));
            br.BaseStream.Position = poSI;
            tablaSecundario.Rows.Clear();

            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                tablaSecundario.Rows.Add(br.ReadInt64(), br.ReadInt32());
            }
            br.Close();
        }
        private void insertaPrimario(string indi, Atributo iPrimario, int posPrimario)
        {
            br.Close();
            bw.Close();
            bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
            TAMPRIM = (4 + 8) * 20;
            Primario p;
            List<Primario> primario = new List<Primario>();
            for (int i = 0; i < dataGridView4.Rows.Count - 2; i++)
            {
                p = new Primario();
                p.dirVal = Convert.ToInt64(dataGridView4.Rows[i].Cells[0].Value);
                p.val = Convert.ToInt32(dataGridView4.Rows[i].Cells[posPrimario + 1].Value);
                primario.Add(p);
            }
            primario = primario.OrderBy(x => x.val).ToList();
            bw.Close();
            bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
            TAMPRIM = (4 + 8) * 20;
            if (primario.Count > 0)
            {
                bw.BaseStream.Position = iPrimario.dameDireccion() + 30 + 1 + 4 + 8 + 5;
                bw.Write(Convert.ToInt64(0));
            }
            bw.Close();
            imprimeAtributo(atributo);
            bw.Close();
            br.Close();
            bw.Close();
            bw.Close();
            bw = new BinaryWriter(File.Open(indi, FileMode.Open));
            foreach (Primario prim in primario)
            {
                bw.Write(prim.val);
                bw.Write(prim.dirVal);
            }
            bw.Close();
            br.Close();
            imprimePrimario(comboBox6.Text);
        }
        public int calculaTamRegistro(string nEntidad)
        {
            br.Close();
            bw.Close();
            int tam = 0;
            long DA = 0;
            int longitud = 0;
            long DSIG = 0;
            long DSIGA = 0;
            string n = "";
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                br.ReadInt64();
                DA = br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == nEntidad)
                {
                    br.BaseStream.Position = DA;
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        br.ReadString();
                        br.ReadChar();
                        longitud = br.ReadInt32();
                        tam = tam + longitud;
                        br.ReadInt64();
                        br.ReadInt32();
                        br.ReadInt64();
                        DSIGA = br.ReadInt64();
                        if (DSIGA != -1)
                            br.BaseStream.Position = DSIGA;
                        else
                            break;
                    }
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            br.Close();
            return (tam + 16);
        }
        public void imprimePrimario(string indi)
        {
            br.Close();
            bw.Close();
            br.Close();
            br.Close();
            br.Close();
            br = new BinaryReader(File.Open(indi + ".idx", FileMode.Open));
            indicePrimario.Rows.Clear();
            int clave;
            int j = 0;
            long TAM = (dataGridView4.Rows.Count - 2) * 12;
            while (br.BaseStream.Position < TAM)
            {
                try
                {
                    clave = br.ReadInt32();
                    if (clave != -1)
                        indicePrimario.Rows.Add(clave, br.ReadInt64());
                    else
                        br.BaseStream.Position += 4;
                    j++;
                }
                catch
                { break; }
            }
            br.Close();
        }
        private Atributo dameIndiceArbol(string nEntidad)
        {
            long DE, DA, DSIG;
            DE = 0;
            DA = 0;
            DSIG = 0;
            string n = "";
            Atributo temp = new Atributo();
            br.Close();
            bw.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                DE = br.ReadInt64();
                DA = br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == nEntidad)
                {
                    if (DA != -1)
                    {
                        br.BaseStream.Position = DA;
                        while (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            temp.nombrate(br.ReadString());
                            temp.ponteTipo(br.ReadChar());
                            temp.ponteLongitud(br.ReadInt32());
                            temp.direccionate(br.ReadInt64());
                            temp.ponteTipoIndice(br.ReadInt32());
                            temp.ponteDirIndice(br.ReadInt64());
                            temp.ponteDirSig(br.ReadInt64());
                            if (temp.dameTI() == 4)
                            {
                                br.Close();
                                return temp;
                            }
                            if (temp.dameDirSig() != -1)
                                br.BaseStream.Position = temp.dameDirSig();
                            else
                                break;
                        }
                    }
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            br.Close();
            return null;
        }
        private Atributo dameIndiceSecundario(string nEntidad)
        {
            long DE, DA, DSIG;
            DE = 0;
            DA = 0;
            DSIG = 0;
            string n = "";
            Atributo temp = new Atributo();
            br.Close();
            bw.Close();
            br.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                DE = br.ReadInt64();
                DA = br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == nEntidad)
                {
                    if (DA != -1)
                    {
                        br.BaseStream.Position = DA;
                        while (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            temp.nombrate(br.ReadString());
                            temp.ponteTipo(br.ReadChar());
                            temp.ponteLongitud(br.ReadInt32());
                            temp.direccionate(br.ReadInt64());
                            temp.ponteTipoIndice(br.ReadInt32());
                            temp.ponteDirIndice(br.ReadInt64());
                            temp.ponteDirSig(br.ReadInt64());
                            if (temp.dameTI() == 3)
                            {
                                br.Close();
                                return temp;
                            }
                            if (temp.dameDirSig() != -1)
                                br.BaseStream.Position = temp.dameDirSig();
                            else
                                break;
                        }
                    }
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            br.Close();
            return null;
        }
        private Atributo dameIndiceSecundario(string nEntidad, string indice)
        {
            long DE, DA, DSIG;
            DE = 0;
            DA = 0;
            DSIG = 0;
            string n = "";
            Atributo temp = new Atributo();
            br.Close();
            bw.Close();
            br.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                DE = br.ReadInt64();
                DA = br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == nEntidad)
                {
                    if (DA != -1)
                    {
                        br.BaseStream.Position = DA;
                        while (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            temp.nombrate(br.ReadString());
                            temp.ponteTipo(br.ReadChar());
                            temp.ponteLongitud(br.ReadInt32());
                            temp.direccionate(br.ReadInt64());
                            temp.ponteTipoIndice(br.ReadInt32());
                            temp.ponteDirIndice(br.ReadInt64());
                            temp.ponteDirSig(br.ReadInt64());
                            if (temp.dameNombre() == indice)
                            {
                                br.Close();
                                return temp;
                            }
                            if (temp.dameDirSig() != -1)
                                br.BaseStream.Position = temp.dameDirSig();
                            else
                                break;
                        }
                    }
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            br.Close();
            return null;
        }
        private Atributo dameIndicePrimario(string nEntidad)
        {
            long DE, DA, DSIG;
            DE = 0;
            DA = 0;
            DSIG = 0;
            string n = "";
            Atributo temp = new Atributo();
            br.Close();
            bw.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                DE = br.ReadInt64();
                DA = br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == nEntidad)
                {
                    if (DA != -1)
                    {
                        br.BaseStream.Position = DA;
                        while (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            temp.nombrate(br.ReadString());
                            temp.ponteTipo(br.ReadChar());
                            temp.ponteLongitud(br.ReadInt32());
                            temp.direccionate(br.ReadInt64());
                            temp.ponteTipoIndice(br.ReadInt32());
                            temp.ponteDirIndice(br.ReadInt64());
                            temp.ponteDirSig(br.ReadInt64());
                            if (temp.dameTI() == 2)
                            {
                                br.Close();
                                return temp;
                            }
                            if (temp.dameDirSig() != -1)
                                br.BaseStream.Position = temp.dameDirSig();
                            else
                                break;
                        }
                    }
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }

            br.Close();
            return null;
        }
        public void calculaTam(string dat)
        {
            br.Close();
            string nEntidad = comboBox6.Text;
            bw.Close();
            primero = 0;
            long DE = 0, DSIG = 0;
            long DA = 0, DSIGA = 0;
            char tipo = ' ';
            int longitud = 0;
            string n = "", nA = "";
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                DE = br.ReadInt64();
                DA = br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == nEntidad)
                {
                    if (DA != -1)
                    {
                        br.BaseStream.Position = DA;
                        while (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            nA = br.ReadString();
                            tipo = br.ReadChar();
                            longitud = br.ReadInt32();
                            DA = br.ReadInt64();
                            br.ReadInt32();
                            br.ReadInt64();
                            DSIGA = br.ReadInt64();
                            primero += longitud;
                            if (DSIGA != -1)
                                br.BaseStream.Position = DSIGA;
                            else
                                break;
                        }
                    }
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            primero += 16;
        }
        public void ponteRegistro(string nEntidad, long DR)
        {
            long DE = 0;
            bw.Close();

            foreach (Entidad e in entidad)
            {
                if (nEntidad == e.dameNombre())
                {
                    DE = e.dameDE();
                    break;
                }
            }
            bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
            bw.BaseStream.Position = DE + 46;
            bw.Write(DR);
            bw.Close();
            imprimeLista(entidad);
            bw.Close();
        }
        private long siguienteRegistro(string dat, Atributo aClave, int TAM, string nEntidad, string nClave, long DREGI)
        {
            long DIR = 0;
            long DSIG = -1;
            long sigE = 0;
            long ANT = 0;
            long DR = 0;
            string n = "";
            char tipo = ' ';
            int compara = 0;
            long p = 0;
            string nomb = " ";
            int pos;
            pos = 0;
            bw.Close();
            br.Close();
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                if (dataGridView3.Columns[i].Name == aClave.dameNombre())
                {
                    pos = i;
                    break;
                }
            }
            br.Close();
            br.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                br.ReadInt64();
                br.ReadInt64();
                DR = br.ReadInt64();
                sigE = br.ReadInt64();
                if (n == nEntidad)
                {
                    break;
                }
                if (sigE != -1)
                    br.BaseStream.Position = sigE;
                else
                    break;
            }
            br.Close();
            br.Close();
            br.Close();
            br.Close();
            br = new BinaryReader(File.Open(dat, FileMode.Open));
            if (DR != -1)
            {
                br.BaseStream.Position = DR;
                p = DR;
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    ANT = br.ReadInt64();
                    for (int i = 0; i < dataGridView3.Columns.Count; i++)
                    {
                        long actual = br.BaseStream.Position;
                        br.Close();
                        tipo = buscaTipo(nEntidad, dataGridView3.Columns[i].Name);

                        br = new BinaryReader(File.Open(dat, FileMode.Open));
                        br.BaseStream.Position = actual;
                        if (tipo == 'E')
                        {
                            br.ReadInt32();
                        }
                        else if (tipo == 'C')
                        {
                            nomb = br.ReadString();

                        }
                        if (i == pos)
                        {
                            compara = nClave.CompareTo(nomb);
                            switch (compara)
                            {
                                case -1:
                                    br.Close();
                                    if (ANT == p)
                                    {
                                        ponteRegistro(comboBox6.Text, DREGI);
                                        bw.Close();
                                        return ANT;
                                    }
                                    else
                                    {
                                        registroAnterior(DIR, DREGI, dat, TAM);
                                        br.Close();
                                        return ANT;
                                    }
                                case 0:
                                    break;
                                case 1:
                                    break;
                            }
                        }
                    }
                    DIR = ANT;
                    DSIG = br.ReadInt64();
                    if (DSIG != -1)
                        br.BaseStream.Position = DSIG;
                    else
                    {
                        registroAnterior(DIR, DREGI, dat, TAM);
                        br.Close();
                        return -1;
                    }
                }
            }
            br.Close();
            return DSIG;
        }
        public void registroAnterior(long ant, long reg, string dat, int TAM)
        {
            br.Close();
            bw.Close();
            bw = new BinaryWriter(File.Open(dat, FileMode.Open));
            bw.BaseStream.Position = ant;
            bw.BaseStream.Position += TAM - 8;
            bw.Write(reg);
            bw.Close();
        }
        private Atributo dameClaveBusqueda(string dat)
        {
            string ent, n;
            long DSIG = 0, DSIGA = 0, DA = 0;
            List<string> atributos = new List<string>();
            List<Atributo> tempAtributo = new List<Atributo>();
            Atributo aClave;
            ent = dat.Replace(".dat", "");
            br.Close();
            bw.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                br.ReadInt64();
                DA = br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == ent)
                {
                    br.BaseStream.Position = DA;
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        atri = new Atributo();
                        atri.nombrate(br.ReadString());
                        atri.ponteTipo(br.ReadChar());
                        atri.ponteLongitud(br.ReadInt32());
                        atri.direccionate(br.ReadInt64());
                        atri.ponteTipoIndice(br.ReadInt32());
                        atri.ponteDirIndice(br.ReadInt64());
                        DSIGA = br.ReadInt64();
                        atri.ponteDirSig(DSIGA);
                        tempAtributo.Add(atri);
                        if (DSIGA != -1)
                            br.BaseStream.Position = DSIGA;
                        else
                            break;
                    }
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            br.Close();
            foreach (Atributo a in tempAtributo)
            {
                if (a.dameTI() == 1)
                {
                    aClave = a;
                    return aClave;
                }
            }
            return null;
        }
        public void imprimeRegistro(string dat)
        {
            br.Close();
            bw.Close();
            string ent, n;
            long DSIG = 0, DSIGA = 0, DA = 0;
            List<string> atributos = new List<string>();
            List<Atributo> tempAtributo = new List<Atributo>();
            ent = dat.Replace(".dat", "");
            br.Close();
            bw.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                br.ReadInt64();
                DA = br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == ent)
                {
                    if (DA != -1)
                    {
                        br.BaseStream.Position = DA;
                        while (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            atri = new Atributo();
                            atri.nombrate(br.ReadString());
                            atri.ponteTipo(br.ReadChar());
                            atri.ponteLongitud(br.ReadInt32());
                            atri.direccionate(br.ReadInt64());
                            atri.ponteTipoIndice(br.ReadInt32());
                            atri.ponteDirIndice(br.ReadInt64());
                            DSIGA = br.ReadInt64();
                            atri.ponteDirSig(DSIGA);
                            tempAtributo.Add(atri);
                            if (DSIGA != -1)
                                br.BaseStream.Position = DSIGA;
                            else
                                break;
                        }
                    }
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            br.Close();
            bw.Close();
            registro.Clear();
            int i = 0;
            int j = 0;
            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Clear();
            dataGridView4.Columns.Add("Dirección Registro", "Dirección Registro");
            foreach (Atributo a in tempAtributo)
            {
                dataGridView4.Columns.Add(a.dameNombre(), a.dameNombre());
            }
            dataGridView4.Columns.Add("Dirección Siguiente", "Dirección Siguiente");
            br = new BinaryReader(File.Open(dat, FileMode.Open));
            long cab;
            long DSIGR = 0;
            cab = dameDireccionRegistro(comboBox6.Text);
            br.Close();
            br.Close();
            br = new BinaryReader(File.Open(dat, FileMode.Open));
            dataGridView4.Rows.Add();
            if (cab != -1)
            {
                br.BaseStream.Position = cab;
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    dataGridView4.Rows.Add();
                    while (i < tempAtributo.Count + 2)
                    {
                        dataGridView4.Rows[j].Cells[i].Value = br.ReadInt64();
                        i++;
                        foreach (Atributo a in tempAtributo)
                        {
                            if (a.dameTipo() == 'E')
                            { dataGridView4.Rows[j].Cells[i].Value = br.ReadInt32(); }
                            else if (a.dameTipo() == 'C')
                            { dataGridView4.Rows[j].Cells[i].Value = br.ReadString(); }
                            i++;
                        }
                        DSIGR = br.ReadInt64();
                        dataGridView4.Rows[j].Cells[i].Value = DSIGR;
                        break;
                    }
                    if (DSIGR != -1)
                        br.BaseStream.Position = DSIGR;
                    else
                        break;
                    i = 0;
                    j++;
                }
                br.Close();
                bw.Close();
            }
            br.Close();
            bw.Close();
        }
        public long dameDireccionRegistro(string nEntidad)
        {
            long DREGISTRO = -1;
            long DSIG = 0;
            string n;
            bw.Close();
            br.Close();
            br.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                br.ReadInt64();
                br.ReadInt64();
                DREGISTRO = br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == nEntidad)
                {
                    br.Close();
                    return DREGISTRO;
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            return DREGISTRO;
        }
        public int buscaTam(string nEntidad, string nAtributo)
        {
            char tipo;
            string n = "";
            string na = "";
            long DSIG = 0, DSIGA = 0;
            int tam = 0;
            long DA = 0;
            bw.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                br.ReadInt64();
                DA = br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                if (n == nEntidad)
                {
                    br.BaseStream.Position = DA;
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {

                        na = br.ReadString();
                        tipo = br.ReadChar();
                        tam = br.ReadInt32();
                        br.ReadInt64();
                        br.ReadInt32();
                        br.ReadInt64();
                        DSIGA = br.ReadInt64();
                        if (na == nAtributo)
                        {
                            br.Close();
                            return tam;
                        }
                        if (DSIGA != -1)
                            br.BaseStream.Position = DSIGA;
                        else
                            break;
                    }
                }
                if (DSIG == -1)
                    break;
            }
            br.Close();
            return 0;
        }
        public char buscaTipo(string nEntidad, string nAtributo)
        {
            char tipo;
            string n = "";
            string na = "";
            long DSIG = 0, DSIGA = 0;
            long DA = 0;
            bw.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                br.ReadInt64();
                DA = br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                if (n == nEntidad)
                {
                    br.BaseStream.Position = DA;
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {

                        na = br.ReadString();
                        tipo = br.ReadChar();
                        br.ReadInt32();
                        br.ReadInt64();
                        br.ReadInt32();
                        br.ReadInt64();
                        DSIGA = br.ReadInt64();
                        if (na == nAtributo)
                        {
                            br.Close();
                            return tipo;
                        }
                        if (DSIGA != -1)
                            br.BaseStream.Position = DSIGA;
                        else
                            break;
                    }
                }
                if (DSIG == -1)
                    break;
            }
            br.Close();
            return ' ';
        }
        private void comboIndice_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboIndice.SelectedItem.ToString())
            {
                case "Sin tipo":
                    tipoIndice = 0;
                    break;
                case "Clave de búsqueda":
                    tipoIndice = 1;
                    break;
                case "Índice primario":
                    tipoIndice = 2;
                    break;
                case "Índice secundario":
                    tipoIndice = 3;
                    break;
                case "Árbol B+":
                    tipoIndice = 4;
                    break;
                case "Multilistas":
                    tipoIndice = 5;
                    break;
            }
        }
        public void inicializaPrimario()
        {
            bw.Close();
            br.Close();

            foreach (string nombre in indi)
            {
                bw = new BinaryWriter(File.Open(nombre, FileMode.Open));
                TAMPRIM = (4 + 8) * 20;
                while (bw.BaseStream.Position < TAMPRIM)
                {
                    bw.Write(Convert.ToInt32(-1));
                    bw.Write(Convert.ToInt64(-1));
                }
                bw.Close();
            }
            button2.Hide();
            bw.Close();
            br.Close();
        }
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dato = "", indice = "";
            foreach (Entidad aux in entidad)
            {
                dato = aux.dameNombre() + ".dat";
                indice = aux.dameNombre() + ".idx";
                datos.Add(dato);
                indi.Add(indice);
            }
            TR = calculaTamRegistro(comboBox6.Text);
            Atributo iPrimario = dameIndicePrimario(comboBox6.Text);
            Atributo iSecundario = dameIndiceSecundario(comboBox6.Text);
            if (iPrimario != null)
                inicializaPrimario();
            if (iSecundario != null)
                inicializaSecundario(comboBox6.Text);
            Atributo iArbol = dameIndiceArbol(comboBox6.Text);
            secundarios = buscaSecundarios(comboBox6.Text);
            int posSecundario = 0;
            List<int> iSecundarios = new List<int>();
            pintaTablaRegistros();
            int j = 0;
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                if (iSecundario != null)
                {
                    if (j < secundarios.Count)
                        if (dataGridView3.Columns[i].Name == secundarios[j])
                        {
                            posSecundario = i;
                            iSecundarios.Add(i);
                            if (j < secundarios.Count)
                                j++;
                            else
                                break;

                        }
                }

            }
            dataGridView3.Columns.Clear();
            pintaTablaRegistros();
            imprimeRegistro(comboBox6.Text + ".dat");
            if (iPrimario != null)
                imprimePrimario(comboBox6.Text);
            comboSecundario.Items.Clear();
            foreach (string secun in secundarios)
            {
                comboSecundario.Items.Add(secun);
            }
            calculaTam(dato);
            if (iArbol != null)
            { imprimeArbol(tree); }
        }
        private void pintaTablaRegistros()
        {
            string nTemp = comboBox6.Text;
            long dAtributo = 0;
            string n;
            long DSIG = 0, DSIGA = 0;
            br.Close();
            bw.Close();
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                br.ReadInt64();
                dAtributo = br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == nTemp)
                {
                    if (dAtributo != -1)
                    {
                        br.BaseStream.Position = dAtributo;
                        while (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            dataGridView3.Columns.Add(br.ReadString(), null);
                            br.ReadChar();
                            br.ReadInt32();
                            br.ReadInt64();
                            br.ReadInt32();
                            br.ReadInt64();
                            DSIGA = br.ReadInt64();
                            if (DSIGA == -1)
                            { break; }
                            else
                                br.BaseStream.Position = DSIGA;
                        }
                    }
                }
                if (DSIG == -1)
                    break;
                else
                    br.BaseStream.Position = DSIG;
            }
        }
        private void modificaEntidad(object sender, EventArgs e)
        {
            string nueva;
            string actual;
            long anterior;
            long actualSig;
            long DE;
            long cab;
            long siguiente;
            long entidadant;
            long DES = 0;
            bw.Close();
            br.Close();
            List<string> nombres = new List<string>();
            foreach (Entidad i in entidad)
                nombres.Add(i.dameNombre());
            ModificaEntidad modifica = new ModificaEntidad(nombres);
            if (modifica.ShowDialog() == DialogResult.OK)
            {
                actual = modifica.dameEntidad();
                nueva = modifica.dameNombre();

                if (nueva != "")
                {
                    while (nueva.Length < 29)
                        nueva += " ";
                    DE = dameDireccionActual(actual);
                    br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
                    cab = br.ReadInt64();
                    br.BaseStream.Position = DE;
                    br.ReadString();
                    br.ReadUInt64();
                    br.ReadUInt64();
                    br.ReadUInt64();
                    DES = br.ReadInt64();
                    br.Close();
                    actualSig = actualSiguiente(actual);
                    anterior = dameAnterior(nueva, actual);
                    siguiente = dameSiguiente(nueva, actual);
                    entidadant = dameEntidadAnterior2(actual, DE);
                    bw.Close();
                    br.Close();
                    bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
                    if (cab == DE)
                    {
                        bw.BaseStream.Position = 0;
                        bw.Write(DES);
                        cabecera.Text = DES.ToString();
                    }
                    if (anterior == 0)
                    {
                        bw.BaseStream.Position = 0;
                        bw.Write(DE);
                        cabecera.Text = DE.ToString();

                    }
                    bw.BaseStream.Position = anterior + 30 + 8 + 8 + 8;
                    if (anterior != 0)
                    { bw.Write(DE); }
                    bw.BaseStream.Position = DE;
                    bw.Write(nueva);
                    bw.BaseStream.Position = DE + 30 + 8 + 8 + 8;
                    bw.Write((long)siguiente);
                    if (DES != siguiente && DES != -1)
                    {
                        bw.BaseStream.Position = entidadant + 30 + 8 + 8 + 8;
                        bw.Write(DES);
                    }
                    if (anterior == 0)
                    { }
                    bw.Close();
                }
                imprimeLista(entidad);
            }
        }
        public long actualSiguiente(string actual)
        {
            br.Close();
            bw.Close();
            string n = "";
            long DE = 0, DSIG;
            long DE2 = 0;
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                DE = br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == actual)
                {
                    br.Close();
                    return DSIG;
                }
                DE2 = DE;
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            return -1;
        }
        public long dameEntidadAnterior2(string actual, long dir)
        {
            br.Close();
            bw.Close();
            string n = "";
            long DE = 0, DSIG;
            long DE2 = 0;
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                DE = br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (DSIG == dir)
                {
                    br.Close();
                    return DE;
                }
                DE2 = DE;
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            return -1;
        }
        public long dameSiguiente(string nEntidad, string actual)
        {
            br.Close();
            bw.Close();
            string n = "";
            long DE = 0, DSIG;
            long DE2 = 0;
            int compara;
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                DE = br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n != actual)
                {
                    compara = n.CompareTo(nEntidad);
                    if (compara == 1)
                    {
                        br.Close();
                        return DE;
                    }
                }
                DE2 = DE;
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            return -1;
        }
        public long dameAnterior(string nEntidad, string actual)
        {
            br.Close();
            bw.Close();
            string n = "";
            long DE = 0, DSIG;
            long DE2 = 0;
            int compara;
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                DE = br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n != actual)
                {
                    compara = n.CompareTo(nEntidad);
                    if (compara == 1)
                    {
                        br.Close();
                        return DE2;
                    }
                    DE2 = DE;
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            return DE;
        }
        public long dameDireccionActual(string nEntidad)
        {
            br.Close();
            bw.Close();
            string n = "";
            long DE, DSIG;
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                DE = br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == nEntidad)
                {
                    br.Close();
                    return DE;
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            br.Close();
            return -1;
        }
        public void inicializaSecundario(string nEntidad)
        {
            br.Close();
            bw.Close();
            int clave = 0;
            bw = new BinaryWriter(File.Open(nEntidad + ".idx", FileMode.Open));
            bw.BaseStream.Position = TAMPRIM;
            Atributo iSecundario = dameIndiceSecundario(nEntidad);
            if (iSecundario != null)
            {
                clave = iSecundario.dameLongitud();
                TAMSEC = ((8 * 20) + clave) * 20;
                bw = new BinaryWriter(File.Open(nEntidad + ".idx", FileMode.Open));
                bw.BaseStream.Position = TAMPRIM;
                while (bw.BaseStream.Position < TAMSEC + TAMPRIM)
                {
                    bw.Write(-1);
                }
                bw.Close();
            }
        }
        private void generaRegistros(object sender, EventArgs e)
        {
            bw.Close();
            imprimeLista(entidad);
            string dato, indice;
            button4.Hide();
            bw.Close();
            Atributo iSecundario = dameIndiceSecundario(comboBox1.Text);
            Atributo iArbol = dameIndiceArbol(comboBox6.Text);
            if (iArbol != null)
                actualizaRaiz(Convert.ToInt64(-1), iArbol);
            foreach (Entidad aux in entidad)
            {
                dato = aux.dameNombre() + ".dat";
                indice = aux.dameNombre() + ".idx";
                datos.Add(dato);
                indi.Add(indice);
            }
            foreach (string nombre in datos)
            {
                bw = new BinaryWriter(File.Open(nombre, FileMode.Create));
                bw.Close();
            }
            foreach (string nombre in indi)
            {
                bw = new BinaryWriter(File.Open(nombre, FileMode.Create));
                Atributo iPrim = dameIndicePrimario(comboBox1.Text);
                if (iPrim != null)
                {
                    TAMPRIM = (4 + 8) * 20;
                    bw = new BinaryWriter(File.Open(nombre, FileMode.Create));
                    while (bw.BaseStream.Position < TAMPRIM)
                    {
                        bw.Write(Convert.ToInt32(-1));
                        bw.Write(Convert.ToInt64(-1));
                    }
                }
                bw.Close();
            }
            button2.Hide();
            bw.Close();
            comboBox6.Enabled = true;
        }

        private void cargaRegistros(object sender, EventArgs e)
        {
            br.Close();
            bw.Close();
            string dato;
            string indice;
            foreach (Entidad aux in entidad)
            {
                dato = aux.dameNombre() + ".dat";
                indice = aux.dameNombre() + ".idx";
                datos.Add(dato);
                indi.Add(indice);
            }
            foreach (string nombre in datos)
            {
                bw = new BinaryWriter(File.Open(nombre, FileMode.Open));
                bw.Close();
            }
            foreach (string nombre in indi)
            {
                bw = new BinaryWriter(File.Open(nombre, FileMode.Open));
                bw.Close();
            }
            comboBox6.Enabled = true;
            button2.Hide();
            button4.Hide();
        }

        private void eliminaRegistro(object sender, EventArgs e)
        {
            string cB = "";
            int claveA = 0;
            long dirA = 0, dirB = 0;
            long direccionRegistro = 0;
            long DANT = -1;
            string dat = comboBox6.Text + ".dat";
            Atributo aClave = dameClaveBusqueda(dat);
            Atributo iArbol;
            int posArbol = 0;
            List<string> claves = new List<string>();
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                if (aClave != null && dataGridView3.Columns[i].Name == aClave.dameNombre())
                {
                    pos = i;
                    break;
                }
            }
            foreach (string arch in datos)
            {
                if (arch == dat)
                {
                    bw = new BinaryWriter(File.Open(dat, FileMode.Open));
                    break;
                }
            }
            iArbol = dameIndiceArbol(comboBox6.Text);
            for (int i = 0; i < dataGridView4.Rows.Count - 2; i++)
            {
                claves.Add(dataGridView4.Rows[i].Cells[pos + 1].Value.ToString());
            }
            EliminaRegistro elimina = new EliminaRegistro(claves);
            if (elimina.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < dataGridView3.Columns.Count; i++)
                {
                    if (iArbol != null)
                    {
                        if (dataGridView3.Columns[i].Name == iArbol.dameNombre())
                        {
                            posArbol = i;
                            break;
                        }
                    }

                }
                cB = elimina.dameClave();
                for (int i = 0; i < dataGridView4.Rows.Count - 1; i++)
                {
                    if (dataGridView4.Rows[i].Cells[pos + 1].Value.ToString() == cB)
                    {
                        direccionRegistro = Convert.ToInt64(dataGridView4.Rows[i].Cells[0].Value);
                        if (iArbol != null)
                        {
                            claveA = Convert.ToInt32(dataGridView4.Rows[i].Cells[posArbol + 1].Value);
                        }
                        break;
                    }
                }
                for (int i = 0; i < dataGridView4.Rows.Count - 2; i++)
                {
                    if (dataGridView4.Rows[i].Cells[dataGridView4.Columns.Count - 1].Value.ToString() == direccionRegistro.ToString())
                    {
                        DANT = Convert.ToInt64(dataGridView4.Rows[i].Cells[0].Value);
                        break;
                    }
                }
                br.Close();
                bw.Close();
                br = new BinaryReader(File.Open(dat, FileMode.Open));
                if (DANT != -1)
                {
                    br.BaseStream.Position = DANT + primero - 8;
                    dirA = br.ReadInt64();
                }
                br.BaseStream.Position = direccionRegistro + primero - 8;
                dirB = br.ReadInt64();
                br.Close();
                bw = new BinaryWriter(File.Open(dat, FileMode.Open));
                if (DANT != -1)
                {
                    bw.BaseStream.Position = DANT + primero - 8;
                    bw.Write(dirB);
                }
                bw.Close();
                long cab = dameDireccionRegistro(comboBox6.Text);
                if (cab == direccionRegistro)
                {
                    br.Close();
                    bw.Close();
                    ponteRegistro(comboBox6.Text, dirB);
                    br.Close();
                    bw.Close();
                }
                if (iArbol != null)
                {
                    eliminaNodo(claveA, direccionRegistro);
                    imprimeArbol(tree);
                    guardaArbol();
                }
                imprimeRegistro(dat);
                imprimePrimario(comboBox6.Text);
            }
        }
        public void eliminaNodo(int clave, long dir)
        {
            Atributo iArbol = dameIndiceArbol(comboBox6.Text);
            long dNodo;
            long dIntermedio = 0;
            long derecha = 0;
            long izquierda = 0;
            bool modifica = false;
            Nodo Intermedio = new Nodo();
            Nodo izq, der;
            bool swap = false;
            long interder, interizq;
            if (tree.dameRaiz() == -1)
            {
                dNodo = tree.buscaNodoE(clave);
                tree.eliminaNodo(dNodo, clave, dir);
                Nodo temp = tree.dameNodo(dNodo);
                if (temp.clave.Count == 0)
                { tree.Remove(temp); }
            }
            else
            {
                if (tree.existeIntermedio() == false)
                {
                    dNodo = tree.buscaNodo(clave);
                    modifica = tree.eliminaNodo(dNodo, clave, dir);
                    if (modifica == false)
                    { }
                    else
                    {
                        derecha = tree.pideDerecho(tree.dameRaiz(), dNodo);
                        izquierda = tree.pideIzquierdo(tree.dameRaiz(), dNodo);
                        if (derecha != -1)
                        {
                            der = tree.dameNodo(derecha);
                            int cl = der.clave[0];
                            tree.agregaDato(dNodo, der.clave[0], der.direccion[0]);
                            tree.actualizaRaizDerecho(derecha);
                            der.clave.RemoveAt(0);
                            der.direccion.RemoveAt(0);
                            imprimeArbol(tree);
                            guardaArbol();
                        }
                        else if (izquierda != -1)
                        {
                            izq = tree.dameNodo(izquierda);
                            int cl = izq.clave[izq.clave.Count - 1];
                            tree.agregaDato(dNodo, izq.clave[izq.clave.Count - 1], izq.direccion[izq.direccion.Count - 1]);
                            tree.actualizaRaizIzquierdo(izquierda);
                            izq.clave.RemoveAt(izq.clave.Count - 1);
                            izq.direccion.RemoveAt(izq.clave.Count - 1);
                            imprimeArbol(tree);
                            guardaArbol();
                        }
                        else
                        {
                            izquierda = tree.existeizquierdo(tree.dameRaiz(), dNodo);
                            derecha = tree.existeDerecho(tree.dameRaiz(), dNodo);
                            if (derecha != -1)
                            {
                                swap = tree.fusionDerecha(dNodo, derecha, tree.dameRaiz());
                                if (swap == true)
                                {
                                    Nodo temp = tree.dameNodo(derecha);
                                    temp.tipo = 'H';
                                    temp.sig = -1;
                                    Nodo nRaiz = tree.dameNodo(tree.dameRaiz());
                                    if (nRaiz.clave.Count == 0)
                                    { tree.Remove(nRaiz); }
                                }
                                else
                                { }
                            }
                            else if (izquierda != -1)
                            {
                                swap = tree.fusionIzquierda(dNodo, izquierda, tree.dameRaiz());
                                if (swap == true)
                                {
                                    Nodo temp = tree.dameNodo(izquierda);
                                    temp.tipo = 'H';
                                    temp.sig = -1;
                                }
                                else { }
                            }
                            else { }
                        }
                    }
                }
                else
                {
                    dNodo = tree.buscaNodoE(clave);
                    modifica = tree.eliminaNodo(dNodo, clave, dir);
                    if (modifica == false) { }
                    else
                    {
                        MessageBox.Show("El arbol se va a fusionar");
                        dIntermedio = tree.dameNodoIntermedio(dNodo);
                        Intermedio = tree.dameNodo(dIntermedio);
                        derecha = tree.pideDerecho(dIntermedio, dNodo);
                        izquierda = tree.pideIzquierdo(dIntermedio, dNodo);
                        if (derecha != -1)
                        {
                            der = tree.dameNodo(derecha);
                            int cl = der.clave[0];
                            tree.agregaDato(dNodo, der.clave[0], der.direccion[0]);
                            der.clave.RemoveAt(0);
                            der.direccion.RemoveAt(0);
                            Intermedio.clave.RemoveAt(0);
                            Intermedio.clave.Add(cl);
                            Intermedio.clave.Sort();
                        }
                        else if (izquierda != -1)
                        {
                            izq = tree.dameNodo(izquierda);
                            int cl = izq.clave[izq.clave.Count - 1];
                            tree.agregaDato(dNodo, izq.clave[izq.clave.Count - 1], izq.direccion[izq.direccion.Count - 1]);
                            izq.clave.Remove(izq.clave[izq.clave.Count - 1]);
                            izq.direccion.Remove(izq.direccion[izq.direccion.Count - 1]);
                            Intermedio.clave.RemoveAt(Intermedio.clave.Count - 1);
                            Intermedio.clave.Add(cl);
                            Intermedio.clave.Sort();
                        }
                        else
                        {
                            izquierda = tree.existeizquierdo(dIntermedio, dNodo);
                            derecha = tree.existeDerecho(dIntermedio, dNodo);
                            if (derecha != -1)
                            {
                                swap = tree.fusionDerecha(dNodo, derecha, dIntermedio);
                                if (swap == true)
                                {
                                    interder = tree.pideIntermedioDerecho(tree.dameRaiz(), dIntermedio);
                                    interizq = tree.pideIntermedioIzquierdo(tree.dameRaiz(), dIntermedio);
                                    if (interder != -1)
                                    { }
                                    else if (interizq != -1)
                                    { }
                                    else
                                    {
                                        izquierda = tree.existeIntermedioIzquierdo(dIntermedio);
                                        derecha = tree.existeIntermedioDerecho(dIntermedio);
                                        if (derecha != -1)
                                        { }
                                        else if (izquierda != -1)
                                        {
                                            swap = tree.fusionaIntermedioIzquierda(dIntermedio, izquierda);
                                            if (swap == true)
                                            {
                                                Intermedio = tree.dameNodo(izquierda);
                                                Intermedio.tipo = 'R';
                                                Intermedio.sig = -1;
                                                imprimeArbol(tree);
                                                guardaArbol();
                                            }
                                            else { }
                                        }
                                    }
                                }
                                else { }
                            }
                            else if (izquierda != -1) { }
                        }
                    }
                }
            }
            imprimeArbol(tree);
            guardaArbol();
            if (tree.dameRaiz() == -1)
            {
                if (tree.Count > 0)
                    actualizaRaiz(tree[0].dirNodo, iArbol);
                else
                    actualizaRaiz(Convert.ToInt64(-1), iArbol);
            }
            else
                actualizaRaiz(arbol.dameRaiz(), iArbol);
            tree.eliminaVacios();
            imprimeArbol(tree);
        }
        public long dameSiguienteR(string actual, long DR, int clave)
        {
            int i;
            int compara = 0;
            long direccion = 0;
            for (i = 0; i < dataGridView4.Rows.Count - 2; i++)
            {
                compara = actual.CompareTo(dataGridView4.Rows[i].Cells[clave + 1].Value.ToString());
                if (compara == 0)
                {
                    direccion = Convert.ToInt64(dataGridView4.Rows[i].Cells[dataGridView4.Columns.Count - 1].Value);
                    return direccion;
                }
            }
            return -1;
        }
        private void modificaRegistro(object sender, EventArgs e)
        {
            List<string> claves = new List<string>();
            Atributo aClave = dameClaveBusqueda(comboBox6.Text + ".dat");
            if (aClave != null)
            {
                for (int i = 0; i < dataGridView3.Columns.Count; i++)
                {
                    if (dataGridView3.Columns[i].Name == aClave.dameNombre())
                    {
                        pos = i;
                        break;
                    }
                }
            }
            for (int i = 0; i < dataGridView4.Rows.Count - 2; i++)
            {
                claves.Add(dataGridView4.Rows[i].Cells[pos + 1].Value.ToString());
            }
            ModificaRegistro mod = new ModificaRegistro(dataGridView3, dataGridView4, claves);
            DataGridView tablatemp = new DataGridView();
            char tipo;
            long pos2 = 0;
            long DireccionRegistro = 0;
            string nRegistro = "";
            string n;
            long DACTUAL = 0;
            long direccionAnteriorActual = 0;
            long direccionSiguienteActual = 0;
            long direccionAnteriorNuevo = 0;
            long direccionSiguienteNuevo = 0;
            long direccionFinal = 0;
            tam = 0;
            if (mod.ShowDialog() == DialogResult.OK)
            {
                n = mod.dameClave();
                for (int i = 0; i < dataGridView4.Rows.Count - 2; i++)
                {
                    if (dataGridView4.Rows[i].Cells[pos + 1].Value.ToString() == n)
                    {
                        bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                        pos2 = (long)dataGridView4.Rows[i].Cells[0].Value;
                        DireccionRegistro = pos2;
                        DACTUAL = dameSiguienteR(n, DireccionRegistro, pos);
                        pos2 += 8;
                        for (int j = 0; j < mod.dameRenglon().Columns.Count; j++)
                        {
                            dataGridView4.Rows[i].Cells[j + 1].Value = mod.dameRenglon().Rows[0].Cells[j].Value;
                            br.Close();
                            bw.Close();
                            tipo = buscaTipo(comboBox6.Text, dataGridView3.Columns[j].Name);
                            bw.Close();
                            bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                            bw.BaseStream.Position = pos2;
                            if (tipo == 'E')
                            {

                                bw.Write(Convert.ToInt32(mod.dameRenglon().Rows[0].Cells[j].Value));
                                pos2 += 4;

                            }
                            else if (tipo == 'C')
                            {
                                int tamC = buscaTam(comboBox6.Text, dataGridView3.Columns[j].Name);
                                bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                                bw.BaseStream.Position = pos2;
                                while (mod.dameRenglon().Rows[0].Cells[j].Value.ToString().Length < tamC - 1)
                                {
                                    mod.dameRenglon().Rows[0].Cells[j].Value += " ";
                                }
                                bw.Write(mod.dameRenglon().Rows[0].Cells[j].Value.ToString());
                                nRegistro = (string)mod.dameRenglon().Rows[0].Cells[j].Value;
                                pos2 += tamC;
                            }
                        }
                        bw.Close();
                        br.Close();
                        direccionAnteriorActual = MetodosRegistros.buscaAnteriorActual(dataGridView4, n, DireccionRegistro, pos);
                        direccionSiguienteActual = MetodosRegistros.buscaSiguienteActual(dataGridView4, n, DireccionRegistro, pos);
                        direccionAnteriorNuevo = MetodosRegistros.buscaAnteriorNuevo(dataGridView4, mod.dameRenglon().Rows[0].Cells[pos].Value.ToString(), DireccionRegistro, pos);
                        direccionSiguienteNuevo = MetodosRegistros.buscaSiguienteNuevo(dataGridView4, mod.dameRenglon().Rows[0].Cells[pos].Value.ToString(), DireccionRegistro, pos);
                        if (direccionAnteriorNuevo == DireccionRegistro)
                        {
                            bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                            bw.BaseStream.Position = direccionAnteriorActual + TR - 8;
                            bw.Write(DireccionRegistro);
                            bw.BaseStream.Position = DireccionRegistro + TR - 8;
                            bw.Write(direccionSiguienteNuevo);
                        }
                        else if (DireccionRegistro == dameDireccionRegistro(comboBox6.Text))
                        {
                            if (direccionAnteriorActual == -1)
                            {
                                ponteRegistro(comboBox6.Text, direccionSiguienteActual);
                                bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                                bw.BaseStream.Position = direccionAnteriorNuevo + TR - 8;
                                bw.Write(DireccionRegistro);
                                bw.BaseStream.Position = DireccionRegistro + TR - 8;
                                bw.Write(direccionSiguienteNuevo);
                            }
                            else if (direccionAnteriorNuevo == 0)
                            {
                                ponteRegistro(comboBox6.Text, DireccionRegistro);
                                bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                                bw.BaseStream.Position = DireccionRegistro + TR - 8;
                                bw.Write(direccionSiguienteNuevo);
                            }
                            else if (direccionAnteriorNuevo == -1)
                            {
                                direccionFinal = MetodosRegistros.buscaUltimoRegistro(dataGridView4);
                                ponteRegistro(comboBox6.Text, direccionSiguienteActual);
                                bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                                bw.BaseStream.Position = direccionFinal + TR - 8;
                                bw.Write(DireccionRegistro);
                                bw.BaseStream.Position = DireccionRegistro + TR - 8;
                                bw.Write(direccionSiguienteNuevo);
                            }
                            else
                            {
                                ponteRegistro(comboBox6.Text, direccionSiguienteActual);
                                bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                                bw.BaseStream.Position = direccionAnteriorNuevo + TR - 8;
                                bw.Write(DireccionRegistro);
                                bw.BaseStream.Position = DireccionRegistro + TR - 8;
                                bw.Write(direccionSiguienteNuevo);
                                bw.BaseStream.Position = direccionAnteriorActual + TR - 8;
                                bw.Write(direccionSiguienteActual);
                            }
                        }
                        else if (DireccionRegistro == MetodosRegistros.buscaUltimoRegistro(dataGridView4))
                        {
                            if (direccionAnteriorNuevo == 0)
                            {
                                ponteRegistro(comboBox6.Text, DireccionRegistro);
                                bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                                bw.BaseStream.Position = DireccionRegistro + TR - 8;
                                bw.Write(direccionSiguienteNuevo);
                                bw.BaseStream.Position = direccionAnteriorActual + TR - 8;
                                bw.Write(direccionSiguienteActual);
                            }
                            else if (direccionAnteriorNuevo == -1)
                            { }
                            else
                            {
                                bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                                bw.BaseStream.Position = direccionAnteriorNuevo + TR - 8;
                                bw.Write(DireccionRegistro);
                                bw.BaseStream.Position = DireccionRegistro + TR - 8;
                                bw.Write(direccionSiguienteNuevo);
                                bw.BaseStream.Position = direccionAnteriorActual + TR - 8;
                                bw.Write(direccionSiguienteActual);
                            }
                        }
                        else
                        {
                            if (direccionAnteriorNuevo == dameDireccionRegistro(comboBox6.Text))
                            {
                                ponteRegistro(comboBox6.Text, DireccionRegistro);
                                bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                                bw.BaseStream.Position = DireccionRegistro + TR - 8;
                                bw.Write(direccionSiguienteNuevo);
                                bw.BaseStream.Position = direccionAnteriorActual + TR - 8;
                                bw.Write(direccionSiguienteActual);
                            }
                            else if (direccionAnteriorNuevo == -1)
                            {
                                bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                                direccionFinal = MetodosRegistros.buscaUltimoRegistro(dataGridView4);
                                bw.BaseStream.Position = direccionFinal + TR - 8;
                                bw.Write(DireccionRegistro);
                                bw.BaseStream.Position = DireccionRegistro + TR - 8;
                                bw.Write(direccionSiguienteNuevo);
                                bw.BaseStream.Position = direccionAnteriorActual + TR - 8;
                                bw.Write(direccionSiguienteActual);
                            }
                            else
                            {
                                bw = new BinaryWriter(File.Open(comboBox6.Text + ".dat", FileMode.Open));
                                bw.BaseStream.Position = direccionAnteriorNuevo + TR - 8;
                                bw.Write(DireccionRegistro);
                                bw.BaseStream.Position = DireccionRegistro + TR - 8;
                                bw.Write(direccionSiguienteNuevo);
                                bw.BaseStream.Position = direccionAnteriorActual + TR - 8;
                                bw.Write(direccionSiguienteActual);
                            }
                        }
                        imprimeRegistro(comboBox6.Text + ".dat");
                        break;
                    }
                }
            }
            imprimeRegistro(comboBox6.Text + ".dat");
        }
        private void tabPage3_Click(object sender, EventArgs e)
        { }
        private void modificaAtributo(object sender, EventArgs e)
        {
            ModificaAtributo mod = new ModificaAtributo(atributo);
            Atributo nuevo;
            bw.Close();
            br.Close();
            if (mod.ShowDialog() == DialogResult.OK)
            {
                nuevo = mod.dameNuevo();
                bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
                bw.BaseStream.Position = nuevo.dameDireccion();
                bw.Write(nuevo.dameNombre());
                bw.Write(nuevo.dameTipo());
                bw.Write(nuevo.dameLongitud());
                bw.Write(nuevo.dameDireccion());
                bw.Write(nuevo.dameTI());
                bw.Write(nuevo.dameDirIndice());
                bw.Write(nuevo.dameDirSig());
                bw.Close();
                imprimeLista(entidad);
                imprimeAtributo(atributo);
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        { }
        private void nuevoProyecto(object sender, EventArgs e)
        {
            string nombre = " ";
            button1.Enabled = true;
            modificar.Enabled = true;
            button3.Enabled = true;
            textBox1.Enabled = true;
            button4.Hide();
            Archivo archivo = new Archivo(nombre);
            AddOwnedForm(archivo);
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                nombre = archivo.nombre + ".bin";
                MessageBox.Show("Nombre de archivo " + nombre);
                bw = new BinaryWriter(File.Open(nombre, FileMode.Create));
                bw.Write((long)8);
                nuevo = true;
                button7.Hide();
                button8.Hide();
                nArchivo = nombre;
                bw.Close();
                bw = new BinaryWriter(File.Open(nombre, FileMode.Open));
            }
        }
        private void comboSecundario_SelectedIndexChanged(object sender, EventArgs e)
        {
            br.Close();
            bw.Close();
            tablaSecundario.Rows.Clear();
            insertaSecundario(comboBox6.Text, iSecundarios);
            br.Close();
            bw.Close();
        }
        private void muestraIndicePrimario(object sender, EventArgs e)
        {
            Atributo iPrimario = dameIndicePrimario(comboBox6.Text);
            int posprimario = 0;
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                if (iPrimario != null)
                {
                    if (dataGridView3.Columns[i].Name == iPrimario.dameNombre())
                    {
                        posprimario = i;
                        break;
                    }
                }
            }
            if (iPrimario != null)
            {
                insertaPrimario(comboBox6.Text + ".idx", iPrimario, posprimario);
                br.Close();
                bw.Close();
                imprimePrimario(comboBox6.Text);
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            bw.Close();
            br.Close();
            Atributo iArbol = dameIndiceArbol(comboBox6.Text);
            if (iArbol != null)
            {
                if (tree != null)
                {
                    if (tree.Count > 0)
                    {
                        if (tree.dameRaiz() != -1)
                        { actualizaRaiz(tree.dameRaiz(), iArbol); }
                        else
                        { actualizaRaiz(tree[0].dirNodo, iArbol); }
                    }
                }
            }
            if (tree != null)
                tree.Clear();
            else
                tree = new Arbol();
            br = new BinaryReader(File.Open(comboBox6.Text + ".idx", FileMode.Open));
            br.BaseStream.Position = TAMSEC;
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                Nodo temp = new Nodo();
                temp.dirNodo = br.ReadInt64();
                temp.tipo = br.ReadChar();
                temp.direccion.Add(br.ReadInt64());
                temp.clave.Add(br.ReadInt32());
                temp.direccion.Add(br.ReadInt64());
                temp.clave.Add(br.ReadInt32());
                temp.direccion.Add(br.ReadInt64());
                temp.clave.Add(br.ReadInt32());
                temp.direccion.Add(br.ReadInt64());
                temp.clave.Add(br.ReadInt32());
                temp.sig = br.ReadInt64();
                temp.direccion.Add(temp.sig);
                tree.Add(temp);
            }
            tree.eliminaCeros();
        }
        private void eliminaEntidad(object sender, EventArgs e)
        {
            br.Close();
            bw.Close();
            string n = textBox1.Text;
            while (n.Length < 29)
                n += " ";
            long anterior;
            anterior = dameEntidadAnterior(n);
            long siguiente;
            string nn = " ";
            siguiente = dameSiguienteEntidad(n);
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            nn = br.ReadString();
            br.Close();
            bw = new BinaryWriter(File.Open(nArchivo, FileMode.Open));
            if (nn == n)
            {
                bw.BaseStream.Position = 0;
                bw.Write(siguiente);
                cabecera.Text = siguiente.ToString();
            }
            bw.BaseStream.Position = anterior + 30 + 8 + 8 + 8;
            if (anterior == -1)
            {
                bw.BaseStream.Position = 0;
            }
            bw.Write(siguiente);
            bw.Close();
            imprimeLista(entidad);
        }
        public long dameSiguienteEntidad(string nEntidad)
        {
            br.Close();
            bw.Close();
            string n = "";
            long DSIG = 0;
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == nEntidad)
                {
                    br.Close();
                    return DSIG;
                }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            return -1;
        }
        public long dameEntidadAnterior(string nEntidad)
        {
            br.Close();
            bw.Close();
            string n = "";
            long DSIG = 0;
            long DE = 0;
            long ANT = 0;
            long DSANT = 0;
            br = new BinaryReader(File.Open(nArchivo, FileMode.Open));
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                n = br.ReadString();
                DE = br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                DSIG = br.ReadInt64();
                if (n == nEntidad)
                { break; }
                if (DSIG != -1)
                    br.BaseStream.Position = DSIG;
                else
                    break;
            }
            br.BaseStream.Position = 0;
            br.BaseStream.Position = br.ReadInt64();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                br.ReadString();
                ANT = br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                DSANT = br.ReadInt64();
                if (DSANT == DE)
                {
                    br.Close();
                    return ANT;
                }
                if (DSANT != -1)
                    br.BaseStream.Position = DSANT;
                else
                    break;
            }
            br.Close();
            return -1;
        }
        #endregion

        ///////////////////////////
        
        #region HASH
        /*
        private void inicializaHash()
        {
            int tam = 0;
            Atributo atr = new Atributo();
            foreach (Atributo a in Entidades[entReg].lAtributos)
            {
                if (a.TipoIndice == 4)
                {
                    tam = a.Longitud;
                    atr = a;
                }
            }
            Console.WriteLine("Tam: " + tam);
            if (tam == 4 && atr.TipoAtributo == 'E')        //es un atributo entero únicamente       
            {
                dataGridView8.Rows.Clear();
                for (int i = 0; i < 7; i++)
                {
                    Entidades[entReg].indHash.Add(new indiceH((i + 1).ToString()));
                }
                if (!File.Exists(nomEnt))
                {
                    arcInd = File.Create(nomEnt);
                    arcInd.Close();
                }
                arcInd = File.Open(nomEnt, FileMode.Open, FileAccess.Write, FileShare.None);
                arcInd.Position = arcInd.Length;
                atr.DirIndice = arcInd.Length;
                ModificaAtributo(atr);
                ActualizaDataGrid2(Entidades[entReg].nombreEntidad);
                BinaryWriter writer = new BinaryWriter(arcInd);
                for (int ca = 0; ca < 7; ca++)
                {
                    long d = -1;
                    writer.Write(d);
                }
                for (int j = 0; j < 7; j++)
                {
                    arcInd.Position = arcInd.Length;
                    Entidades[entReg].indHash[j].apuntador = arcInd.Length;

                    dataGridView8.Rows.Add(Entidades[entReg].indHash[j].cajon, (j * 1040) + 56);
                    long l = -1;
                    for (int i = 0; i < 86; i++)
                    {
                        int n = 0;
                        long d = -1;
                        writer.Write(n);
                        writer.Write(d);
                    }
                    writer.Write(l);
                }
                arcInd.Close();
            }
        }
        */
        #endregion
        
    }
}
    
