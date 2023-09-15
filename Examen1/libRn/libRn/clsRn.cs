using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace libRn
{
    public class clsRn
    {

        #region "Atributos"
        private int intTipoEst;
        private float fltProm;
        private float fltValCredito;
        private int intCredit;
        private float fltDesc;
        private string strError;
        #endregion
        #region "Constructor"
        public clsRn()
        {
            intCredit = 0;
            fltProm = 0;
            fltValCredito = 0;
            intCredit = 0;
            fltDesc = 0;
            strError = string.Empty; // = ""
        }
        #endregion
        #region "Propiedades"
        //Entrada
        public int tipoEstudiante
        {
            set { intTipoEst = value; }
        }

        public float Promedio
        {
            set { fltProm = value; }
        }

        //Salida
        public float ValorCredito
        {
            get { return fltValCredito; }
        }

        public int numCreditos
        {
            get { return intCredit; }
        }

        public float Descuento
        {
            get { return fltDesc; }
        }

        public string Error
        {
            get { return strError; }
        }
        #endregion
        #region "Metodos privados"
        private bool Validar()
        {
            if (intTipoEst != 1 && intTipoEst != 2)
            {
                strError = "Tipo de estudiante no válido";
                return false;
            }
            if (fltProm < 0 || fltProm > 5)
            {
                strError = "Promedio de nota no válido";
                return false;
            }
            if (fltProm < 2.5 && intTipoEst == 1)
            {
                strError = $"No puedes matricularte, promedio ({fltProm}) inferior a 2.5";
                return false;
            }
            return true;
        }
        private bool leerArchivo()
        {
            if (!Validar())
                return false;
            try
            {
                string strPath = AppDomain.CurrentDomain.BaseDirectory + @"Parcial1.txt";
                int intCant = 0;
                string strLinea;
                string[] vectorLinea;
                string strCodigo;
                float fltNota;
                intCant = File.ReadAllLines(strPath).Length;
                if (intCant <= 0)
                    return true;
                StreamReader Archivo = new StreamReader(@strPath); //Crear objeto para leer el archivo
                while ((strLinea = Archivo.ReadLine()) != null)      //Leer Linea * Linea el archivo
                {
                    vectorLinea = strLinea.Split(':');
                    strCodigo = vectorLinea[0];                        //Tipo de estudiante (Programa)
                    fltNota = Convert.ToSingle(vectorLinea[1]);        //Promedio mínimo de Nota
                    if (strCodigo == intTipoEst.ToString() && fltProm >= fltNota)
                    {
                        fltValCredito = Convert.ToSingle(vectorLinea[2]);  //Valor crédito
                        intCredit = Convert.ToInt16(vectorLinea[3]);  //Cantidad Créditos
                        fltDesc = Convert.ToSingle(vectorLinea[4]);  //Porcentaje Dscto
                        break;
                    }
                }
                Archivo.Close();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        #endregion
        #region "Metodos publicos"
        public bool hallarDatos()
        {
            return leerArchivo(); //Encapsulamiento
        }
        #endregion
    }
}
