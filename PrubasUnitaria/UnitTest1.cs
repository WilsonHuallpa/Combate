
using biblioteca;

namespace PrubasUnitaria
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void IntanciandoUnPersonaje()
        {
            Hechicero personaje1 = new Hechicero(2, "thorw", 2);
            Guerrero personaje2 = new Guerrero(2, "thorw", 0);
        }

        [TestMethod]
        public void RevisarPuntosDeVida()
        {
            Hechicero personaje1 = new Hechicero(2, "luna", 1);

            personaje1.RecibirAtaque(1000);

            Assert.IsTrue(personaje1.PuntosDeVida == 0);
        }
        [TestMethod]
        public void IniciarCorrectamentLosPuntos()
        {
            Hechicero personaje1 = new Hechicero(2, "thorw");
            Guerrero personaje2 = new Guerrero(2, "thorw");

            Assert.AreEqual(personaje1.PuntoDeDefensa, 100);
            Assert.AreEqual(personaje2.PuntoDeDefensa, 110);

        }

    }
}