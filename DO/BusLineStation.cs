using System;
using System.Collections.Generic;
using System.Text;
namespace DO
{
    /// <summary>
    /// Bus Line Station calss
    /// </summary>
    public class BusLineStation
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_identificationNumber"></param>
        /// <param name="_codeStation"></param>
        /// <param name="_numberStationInLine"></param>
        /// <param name="_active"></param>
        public BusLineStation(string _identificationNumber,string _codeStation,int _numberStationInLine,bool _active)
        {
            identificationNumber = _identificationNumber;
            codeStation = _codeStation;
            numberStationInLine = _numberStationInLine;
            active = _active;
        }
        private static string identificationNumber;//Identification number
        private string codeStation;// bus station code
        private int numberStationInLine;//The station number on the line
        private bool active;//A field that brings me the status of a bus line whether it is active or not
        /// <summary>
        /// sets and gets Functions
        /// </summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public string IdentificationNumber
        {
            get { return identificationNumber; }
            set { identificationNumber = value; }
        }
        public string CodeStation
        {
            set { codeStation = value; }
            get { return codeStation; }
        }
        public int NumberStationInLine
        {
            set { numberStationInLine = value; }
            get { return numberStationInLine; }
        }
    }
}
