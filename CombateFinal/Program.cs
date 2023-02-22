using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biblioteca;
namespace CombateFinal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger log = new Logger("MiArchivo.log");
            try
            {
                Personaje personaje1 = PersonajeDAO.ObtenerPersonajePorId(1);
                Personaje personaje2 = PersonajeDAO.ObtenerPersonajePorId(2);
                //Personaje personaje1 = new Hechicero(3, "");

                personaje1.AtaqueLanzado += MostrarAtaqueLanzado;
                personaje1.AtaqueRecibido += MostrarAtaqueRecibido;
                personaje2.AtaqueLanzado += MostrarAtaqueLanzado;
                personaje2.AtaqueRecibido += MostrarAtaqueRecibido;

                Combate combate = new Combate(personaje1, personaje2);
                combate.RondaIniciada += IniciarRonda;
                combate.CombateFinalizado += FinalizarCombate;

                Console.WriteLine("¡FIGHT!");


                combate.IniciarCombate().Wait();
            }catch(BusinessException e)
            {
                Console.WriteLine(e.Message, e);
                log.GuardarLog(e.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message , ex);
                log.GuardarLog(ex.ToString());
            }
           

        }
        static void IniciarRonda(IJugador atacante, IJugador atacado)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"¡{atacante} ataca a {atacado}!");
        }

        static void FinalizarCombate(IJugador ganador)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Combate finalizado. El ganador es {ganador}.");
        }

        static void MostrarAtaqueLanzado(Personaje personaje, int puntosDeAtaque)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{personaje} lanzó un ataque de {puntosDeAtaque} puntos.");
        }

        static void MostrarAtaqueRecibido(Personaje personaje, int puntosDeAtaque)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{personaje} recibió un ataque por {puntosDeAtaque} puntos. Le quedan {personaje.PuntosDeVida} puntos de vida.");
        }
    }
}
