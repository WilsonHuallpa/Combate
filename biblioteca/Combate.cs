using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace biblioteca
{
    public sealed class Combate
    {
        private IJugador atacado;
        private IJugador atacante;
        private static Random random;
        public delegate void manejador(IJugador jugador1, IJugador jugador2);
        public delegate void manejador2(IJugador jugador);
        public event manejador RondaIniciada;
        public event manejador2 CombateFinalizado;


        static Combate()
        {
            random = new Random();
        }

        public Combate(IJugador jugador1, IJugador jugador2)
        {
            this.atacante = SeleccionarPrimerAtacante(jugador1, jugador2);
            if (atacante != jugador1)
            {
                this.atacado = jugador1;
            }
            else
            {
                this.atacado = jugador2;
            }
        }
        
        private void Combatir()
        {
            IJugador ganador;
            do
            {
                IniciarRonda();
                ganador = EvaluarGanador();
            } while (ganador == null);

            if (CombateFinalizado != null)
            {
                CombateFinalizado(ganador);
                ResultadoCombate combate = new ResultadoCombate(ganador.ToString(), this.atacado.ToString(), DateTime.Now);
                using (StreamWriter streamWriter = new StreamWriter("alumno.xml"))
                {
                    //serializar y guardar datos de toda veces que se gano.
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ResultadoCombate));
                    xmlSerializer.Serialize(streamWriter, combate);
                }
            }
            else
            {
                Console.WriteLine("ERROR.. El Evento No tiene subcriptores.");
            }
        }
        private IJugador EvaluarGanador()
        {
            if (this.atacado.PuntosDeVida == 0)
            {
                return this.atacante;
            }
            else
            {
                IJugador auxJugador = this.atacado;
                this.atacado = this.atacante;
                this.atacante = auxJugador;
                return null;

            }
        }
        public Task IniciarCombate()
        {
            Task tarea = Task.Run(Combatir);

            return tarea;
        }
        private void IniciarRonda()
        {
            if (RondaIniciada != null)
            {
                RondaIniciada(this.atacante, this.atacado);
                int puntosAtaque = atacante.Atacar();
                atacado.RecibirAtaque(puntosAtaque);
            }
            else
            {
                Console.WriteLine("ERROR... El evento Iniciar Ronda no tiene subscriptores.");
            }
        }
        private IJugador SeleccionarJugadorAleatoriamente(IJugador jugador1, IJugador jugador2)
        {
            LadosMoneda Moneda = random.TirarUnaMoneda();
            return (Moneda == LadosMoneda.cara) ? jugador1 : jugador2; 
        }
        private IJugador SeleccionarPrimerAtacante(IJugador jugador1, IJugador jugador2)
        {
            if (jugador1.Nivel != jugador2.Nivel)
            {
                if (jugador1.Nivel < jugador2.Nivel)
                {
                    return jugador1;
                }
                else
                {
                    return jugador2;
                }
            }
            else
            {
                return this.SeleccionarJugadorAleatoriamente(jugador1, jugador2);
            }
        }


    }
}
