using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;
namespace BLAPI
{
    public interface IBL
    {
        #region Bus Line 
        BusLine GetBusLine(int id);
        IEnumerable<BusLine> GetAllBusLines();
        IEnumerable<BusLine> GetAllBusLinesSortByNumberOfStations();
        IEnumerable<int> GetNumberLines();
        IEnumerable<BusLine> GetBusLineBy(Predicate<BusLine> predicate);
        void AddABusLine(BusLine busLine);
        void AddBusStation(BusLine AddToLine,BusLineStation busLineStation);
        void DeleteBusLineStation(BusLine DeleteFromLine,BusLineStation busLineStation);
        void DeleteBusLine(BusLine busLine);
        IEnumerable<IGrouping<string, BusLine>> GetAllBusLinesGroupByArea();

        #endregion
    }
}
