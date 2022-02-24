using CsvHelper.Configuration.Attributes;
using Lab02_ed_22.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab02_ed_22.Models
{
    public class EquipoModel
    {
        [MaxLength(25)]
        [MinLength(1)]
        [Index(0)]
        public string NombreEquipo { get; set; }

        [MaxLength(30)]
        [MinLength(2)]
        [Index(1)]
        public string Coach { get; set; }

        [MaxLength(25)]
        [MinLength(1)]
        [Index(2)]
        public string Liga { get; set; }

        [Index(3)]
        public DateTime FechaCreacion { get; set; }

        public static bool Guardar(EquipoModel equipo)
        {
            Data.Instance.equipoList.Add(equipo);
            return true;
        }

        public static bool Editar(string nombre, EquipoModel equipo)
        {
            var posicion = Data.Instance.equipoList.FindIndex(modelo => modelo.NombreEquipo == nombre);
            Data.Instance.equipoList[posicion] = new EquipoModel
            {
                NombreEquipo = nombre,
                Coach = equipo.Coach,
                Liga = equipo.Liga,
                FechaCreacion = equipo.FechaCreacion
            };
            return true;
        }

        public static bool Eliminar(string nombre)
        {
            int posicion = Data.Instance.equipoList.FindIndex(modelo => modelo.NombreEquipo == nombre);
            Data.Instance.equipoList.RemoveAt(posicion);
            for (int i = 0; i < Data.Instance.jugadorlist.Count; i++)
            {
                if (Data.Instance.jugadorlist[i].Equipo == nombre)
                {
                    JugadorModel.Eliminar(Data.Instance.jugadorlist[i].Nombre);
                    i--;
                }
            }
            return true;
        }
    }
}
