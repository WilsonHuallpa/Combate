using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    public class Guerrero : Personaje
    {
        public Guerrero(decimal id, string nombre) : base(id, nombre)
        {
            this.AplicarBeneficiosDeClase();
        }
        public Guerrero(decimal id, string nombre, short nivel) : base(id, nombre, nivel)
        {
            
        }

        protected override void AplicarBeneficiosDeClase()
        {
            int defensa = this.puntosDeDefensa;
            int diesPorciento = (defensa * 10) / 100;
            this.puntosDeDefensa += diesPorciento;
        }
    }
}
