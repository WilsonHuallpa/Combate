using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class Logger
    {
        private string ruta;
        
        public void GuardarLog(string texto)
        {
            string datos = texto;
            try
            {
                if (!string.IsNullOrEmpty(this.ruta) && !string.IsNullOrEmpty(datos))
                {
                    using (StreamWriter sw = new StreamWriter(this.ruta, true))
                    {
                        sw.WriteLine(datos);
                    }
                }
                //chequear
            }
            catch (Exception)
            {
                throw new Exception("Error al intentar guardar archivo de texto");
            }
        }
        public Logger(string ruta)
        {
            this.ruta = ruta;
        }
    }
}
