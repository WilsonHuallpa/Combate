using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class Hechicero : Personaje
    {
        public Hechicero(decimal id, string nombre) : base(id, nombre)
        {
            AplicarBeneficiosDeClase();
        }
        public Hechicero(decimal id, string nombre, short nivel) : base(id, nombre, nivel)
        {
        }

        protected override void AplicarBeneficiosDeClase()
        {
            int poder = this.puntosDePorder;
            int diesPorciento = (poder * 10) / 100;
            this.puntosDePorder += diesPorciento;
        }
    }
}
