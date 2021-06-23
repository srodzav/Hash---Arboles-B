using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_Datos
{
    class Atributo
    {
    	//Declaracion de variavbles
    	private char[] nombre;	// nombre del atributo
    	private char tipo;	// tipo del atributo
    	private int longitud;	// longitud del atributo 
    	private long DA;		// Dirección del atributo
    	private int TI;			// Tipo de Indice
    	private long DI;		// Dirección del indice
    	private long DSIG;		// Dirección siguiente atributo

    	//Constructor
    	public Atributo(){
    		nombre = new char[30];
    		tipo = ' ';
    		longitud = 0;
    		DA = -1;
    		TI = 0;
    		DI = -1;
    		DSIG = -1;
    	}

        // Get's y Set's 
        public void nombrate(string _nombre)
        {
            this.nombre = _nombre.ToCharArray();
        }
        public void ponteTipo(char t){
            this.tipo = t;
        }
        public void ponteLongitud(int l){
            this.longitud = l;
        }
        public void direccionate(long dir)
        {
            this.DA = dir;
        }
        public void ponteTipoIndice(int tipI){
            this.TI = tipI;
        }
        public void ponteDirIndice(long _DI){
            this.DI = _DI;
        }
        public void ponteDirSig(long _sig){
            this.DSIG = _sig;
        }
        public long dameDireccion()
        {
            return this.DA;
        }
        public string dameNombre()
        {
            string n = new string(this.nombre);
            return n;
        }
      
        public char dameTipo()
        {
            return this.tipo;
        }
        public int dameLongitud(){
            return this.longitud;
        }
        public int dameTI(){
            return this.TI;
        }  
        public long dameDirIndice(){
            return this.DI;
        }
        public long dameDirSig(){
            return this.DSIG;
        }
      

    }
}

