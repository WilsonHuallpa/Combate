using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace biblioteca
{
    public delegate void Manejador(Personaje personaje, int puntos);
    public abstract class Personaje : IJugador
    {
        private decimal id;
        private short nivel;
        private string nombre;
        protected int puntosDeDefensa;
        protected int puntosDePorder;
        protected int puntosDeVida;
        private static Random random;
        private string titulo;
        public const int Maximo = 100;
        public const int Minimo = 1;
        public event Manejador AtaqueLanzado;
        public event Manejador AtaqueRecibido;


        public string Titulo { set => titulo = value; }

        public short Nivel { get => nivel; }

        public int PuntosDeVida { get => puntosDeVida; }
        public int PuntoDeDefensa { get => puntosDeDefensa; }

        static Personaje()
        {
            random = new Random();
        }
        public Personaje(decimal id, string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentNullException("Error en Nombre, Incresar correctamente.");
            }

            this.id = id;
            this.nombre = nombre.Trim();
            this.puntosDeDefensa = 100;
            this.puntosDeVida = 500;
            this.puntosDePorder = 100;
            this.nivel = 1;
            this.titulo = "";

        }
        public Personaje(decimal id, string nombre, short nivel):this(id,nombre)
        {
            if (nivel >= Minimo && nivel <= Maximo)
            {
                this.nivel = nivel;
                this.puntosDeDefensa *= nivel;
                this.puntosDePorder *= nivel;
                this.puntosDeVida *= nivel;
            }
            else
            {
                throw new BusinessException("Error... Nivel no se encuentra en el rango permitido.");
            }
           
        }
        protected abstract void AplicarBeneficiosDeClase();

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Personaje aux = (Personaje)obj;
                return aux.GetHashCode() == this.GetHashCode();
            }
        }
        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
        public static bool operator == (Personaje personaje, Personaje otroPersonaje)
        {
            return (personaje != null && personaje.Equals(otroPersonaje));
        }
        public static bool operator !=(Personaje personaje, Personaje otroPersonaje)
        {
            return !(personaje == otroPersonaje);
        }

        public override string ToString()
        {
            if (this.titulo != "")
            {
                return $"{this.nombre} , {this.titulo}";
            }
            return $"{this.nombre}";
        }

        public int Atacar()
        {
            Thread.Sleep(random.Next(1000,5000));
            int puntosDeAtaque;
            int diesPorciento = (this.puntosDePorder * 10) / 100;
            puntosDeAtaque = random.Next(diesPorciento, this.puntosDePorder);
            
            if(AtaqueLanzado != null)
            {
                AtaqueLanzado(this, puntosDeAtaque);
            }
            else
            {
                Console.WriteLine("El eventeo Ataque Lanzado no tiene suscriptores.");
            }

            return puntosDeAtaque;
        }

        public void RecibirAtaque(int puntosDeAtaque)
        {
            int puntosDefendidos;
            int diesPorciento = (this.puntosDeDefensa * 10) / 100;
            puntosDefendidos = random.Next(diesPorciento, this.puntosDeDefensa);
            puntosDeAtaque -= puntosDefendidos;

            this.puntosDeVida -= puntosDeAtaque;
            if (this.puntosDeVida < 0)
            {
                this.puntosDeVida = 0;
            }
            if (AtaqueRecibido != null)
            {
                AtaqueRecibido(this, puntosDeAtaque);
            }
            else
            {
                Console.WriteLine("El eventeo Ataque Recibido no tiene suscriptores.");
            }

        }
    }
}
