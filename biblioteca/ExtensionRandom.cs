using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public enum LadosMoneda
    {
        cara = 1,
        ceca = 2
    }
    public static class ExtensionRandom
    {
        public static LadosMoneda TirarUnaMoneda(this Random random )
        {
            LadosMoneda[] values = (LadosMoneda[])Enum.GetValues(typeof(LadosMoneda));
            return values[random.Next(0, values.Length)];
        } 
    }
}
