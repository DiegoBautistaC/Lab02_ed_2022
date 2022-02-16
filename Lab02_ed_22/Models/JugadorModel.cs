﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Lab02_ed_22.Helpers;

namespace Lab02_ed_22.Models
{
    public class JugadorModel
    {
        [MaxLength(50)]
        [MinLength(2)]
        public string Nombre { get; set; }
        [MaxLength(50)]
        [MinLength(2)]
        public string Apellido { get; set; }
        [MaxLength(50)]
        [MinLength(2)]
        public string Rol { get; set; }
        public double KDA { get; set; }
        public int CreepScore { get; set; }
        [MaxLength (99)]
        public string Equipo { get; set; }

        public static bool Guardar(JugadorModel modelo)
        {
            Data.Instance.jugadorlist.Add(modelo);
            return false;
        }
        public static bool Editar(string Nombre, JugadorModel model)
        {
            var position = Data.Instance.jugadorlist.FindIndex(jugador => jugador.Nombre == Nombre);
            Data.Instance.jugadorlist[position] = new JugadorModel
            {

                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Rol = model.Rol,
                KDA = model.KDA,
                CreepScore = model.CreepScore,
                Equipo = model.Equipo
            };
            return true;
        }
        public static bool Eliminar(string Nombre)
        {
            var position = Data.Instance.jugadorlist.FindIndex(jugador=> jugador.Nombre == Nombre);
            Data.Instance.jugadorlist.RemoveAt(position);

            return false;
        }
    }
}