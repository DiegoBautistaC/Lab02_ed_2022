using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Nodo<T>
    {
        public T Valor { get; set; }
        public Nodo<T> Anterior { get; set; }
        public Nodo<T> Siguiente { get; set; }

        public Nodo(T valor)
        {
            Valor = valor;
            Anterior = null;
            Siguiente = null;
        }

    }
}
