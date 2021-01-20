using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// Enums Class
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// Zones enum for bus lines
        /// Status enum for buses
        /// </summary>
        public enum Zones { Itamar, Zeev_hill, Alon_Shvut, Gush_Dan, Gush_Etzion, Galilee, Jerusalem_Corridor, Beer_Sheva, General, Zefat, Ramat_Gan }
        public enum Status { Ready_to_go, In_the_middle_of_A_ride, refueling, In_treatment, Dangerous }
    }
}
