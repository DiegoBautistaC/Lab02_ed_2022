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
        public static Nodo<T> Cola = null;
        public static int Tamaño = 0;

        public void Agregar(T valor)
        {
            var nuevoNodo = new Nodo<T>(valor);

            if (Tamaño == 0)
            {
                Cabeza = nuevoNodo;
                Cola = nuevoNodo;
            }
            else
            {
                if (Tamaño == 1)
                {
                    Cola.Anterior = nuevoNodo;
                }
                Cabeza.Anterior = nuevoNodo;
                nuevoNodo.Siguiente = Cabeza;
                Cabeza = nuevoNodo;
            }
            Tamaño++;
        }
    }
}
