using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_Datos
{
    class Entidad
    {
    	private string nombre; 	// nombre Entidad
    	private long DE;		// Dirección Entidad		
    	private long DA;		// Dirección Atributo
    	private long DD;		// Dirección del Registro
    	private long DSIG;		// Direccioón Siguiente Entidad
        private int i;

    	// Constructor 
    	public Entidad()
    	{
            nombre = "";
    		DE = -1;
    		DA = -1;
    		DD = -1;
    		DSIG  = -1;
            i = 0;

    	}
    	//Get's y Set's
    	public void nombrate(string n){
            this.nombre = n;
        }
        public void direccionate(long dir){
            this.DE = dir;
        }
        public void ponteDireccionAtributo(long dir){
            this.DA = dir;
        }
        public void ponteDireccionRegistro(long dir){
            this.DD = dir;
        }
        public void ponteDireccionSig(long dir){
            this.DSIG = dir;
        }
        public string dameNombre() {
            // Se convierte la candena nombre a tipo string
          
            return nombre; 
        }
        public long dameDE() { return this.DE; }
        public long dameDA(){ return this.DA;}
        public long dameDD(){ return this.DD;}
        public long dameDSIG() { return this.DSIG; }
            
    }
}
