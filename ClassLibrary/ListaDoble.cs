using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ListaDoble<T>
    {
        public static Nodo<T> Cabeza = null;
        public static int Tamaño = 0;

        public static void Agregar(T valor)
        {
            if (Tamaño == 0)
            {
                Cabeza = new Nodo<T>(valor);
            }
            else
            {

            }
        }
    }
}
