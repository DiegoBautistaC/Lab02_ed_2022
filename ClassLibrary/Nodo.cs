using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    class Nodo<T>
    {
        public T Valor { get; set; }
        public Nodo<T> Anterior;
        public Nodo<T> Siguiente;
    }
}
