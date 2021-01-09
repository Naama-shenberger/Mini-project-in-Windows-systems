using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;

namespace BL
{
    public static class DeepCopyUtilities
    {
        /// <summary>
        /// Deep copying
        /// The function contains a loop that passes over all the Properties in variable S and copies the Properties to T
        /// In case T does not have the Propertie it continues in a loop
        /// in case it does have it it copies the Properties by setValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void CopyPropertiesTo<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = typeof(S).GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);
            }
        }
        public static object CopyPropertiesToNew<S>(this S from, Type type)
        {
            ///Activator
            ///Creates an instance of the specified type using the constructor that best matches the specified parameters.
            ///Contains methods to create types of objects locally or remotely
            ///or obtain references to existing remote objects. This class cannot be inherited.
            object to = Activator.CreateInstance(type); // new object of Type
            from.CopyPropertiesTo(to);
            return to;
        }
        /// <summary>
        /// Function that copies bus line station information
        /// The function checks if the line station has a tracking station 
        /// if the station has the Function enters the data otherwise enters that the time is zero and distance -1
        /// </summary>
        /// <param name="busStation"></param>
        /// <param name="dal"></param>
        /// <returns></returns>
        public static BO.BusLineStation CopyToStationInLine(DO.BusLineStation busStation,IDal dal)
        {
            BO.BusLineStation result = (BO.BusLineStation)busStation.CopyPropertiesToNew(typeof(BO.BusLineStation));
            var station = from sin in dal.ConsecutivesStations()
                          where sin.StationCodeOne == busStation.BusStationKey || sin.StationCodeTwo == busStation.BusStationKey
                          select sin;
            if (station.Count() != 0)
            {
                if (busStation.NumberStationInLine == 1)
                {
                    result.AverageTravelTime = new TimeSpan(0, 0, 0);
                    result.Distance = 0;
                }
                else
                {
                    result.AverageTravelTime = station.ToList()[0].AverageTravelTime;
                    result.Distance = station.ToList()[0].Distance;
                }
            }
            else
            {
                result.AverageTravelTime = new TimeSpan(0,0,0);
                result.Distance = -1;
            }
            return result;
        }
        /// <summary>
        /// Function that copies bus line station information
        /// </summary>
        /// <param name="busStation"></param>
        /// <returns></returns>
        public static BO.BusLineStation CopyToStationInLine(DO.BusLineStation busStation)
        {
            BO.BusLineStation result = (BO.BusLineStation)busStation.CopyPropertiesToNew(typeof(BO.BusLineStation));
            return result;
        }
        /// <summary>
        /// Function that copies bus line information
        /// </summary>
        /// <param name="busLine"></param>
        /// <param name="sic"></param>
        /// <returns></returns>
        public static BO.BusLineInStation CopyToLineInStation(this DO.BusLine busLine, BO.BusLineInStation sic)
        {
            BO.BusLineInStation result = (BO.BusLineInStation)busLine.CopyPropertiesToNew(typeof(BO.BusLineInStation));
            return result;
        }
        /// <summary>
        /// Function that copies Line Rides information
        /// </summary>
        /// <param name="lineRide"></param>
        /// <returns></returns>
        public static BO.LineRides CopyToLineRide(DO.LineRide lineRide)
        {
            BO.LineRides result = (BO.LineRides)lineRide.CopyPropertiesToNew(typeof(BO.LineRides));
            return result;
        }

    }
}


