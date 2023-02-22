using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class BusinessException : Exception
    {
        public BusinessException(string mensaje):base(mensaje)
        {
                
        }
        public BusinessException(string mensaje, Exception innerExection):base(mensaje, innerExection)
        {
                
        }
    }
}
