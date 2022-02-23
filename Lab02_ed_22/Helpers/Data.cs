using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab02_ed_22.Models;
using Lab02_ed_22.Controllers;
using ClassLibrary;

namespace Lab02_ed_22.Helpers
{
    public class Data
    {
        private static Data _instance = null;
        public static Data Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Data();

                }
                return _instance;
            }
        }


        public List<JugadorModel>jugadorlist = new List<JugadorModel>
        {
            new JugadorModel
            {
                Nombre = "Douglas",
                Apellido = "Salazar",
                Rol = "Jungla",
                KDA = 2.99,
                CreepScore = 10,
                Equipo = "SKT1"
            }
        };

        public List<EquipoModel> equipoList = new List<EquipoModel>
        {
            new EquipoModel
            {
                NombreEquipo = "Team Queso",
                Coach = "Prod1gy",
                Liga = "LEC",
                FechaCreacion = Convert.ToDateTime("29/05/2019")
            }
        };
    }
}
    
