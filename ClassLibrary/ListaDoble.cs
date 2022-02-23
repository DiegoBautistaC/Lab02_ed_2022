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

        public delegate bool Delegado<D>(D valor1, D valor2);

        public bool Agregar(T valor)
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
            return true;
        }

        public bool RemoverEn(int indice)
        {
            if (indice >= 0 || indice < Tamaño)
            {
                int i = 0;
                for (Nodo<T> aux = Cabeza; aux != null; aux = aux.Siguiente)
                {
                    if (i == indice)
                    {
                        if (i == 0)
                        {
                            if (Tamaño == 1)
                            {
                                Cabeza = null;
                                Cola = null;
                            }
                            else
                            {
                                Cabeza.Siguiente.Anterior = null;
                                Cabeza = aux.Siguiente;
                            }
                        }
                        else if (i == (Tamaño - 1))
                        {
                            Cola = aux.Anterior;
                            Cola.Siguiente = null;
                        }
                        else
                        {
                            aux.Siguiente.Anterior = aux.Anterior;
                            aux.Anterior.Siguiente = aux.Siguiente;
                        }
                        Tamaño--;
                        return true;
                    }
                    i++;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public int EncontrarIndice(T valor)
        {
            
            return -1;
        }
    }
}
