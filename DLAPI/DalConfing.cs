using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DalApi
{
    /// <summary>
    /// Class for processing config.xml file and getting from there
    /// information which is relevant for initialization of DalApi
    /// </summary>
    static class DalConfing
    {
        internal static string DalName;
        internal static Dictionary<string, string> DalPackages;

        /// <summary>
        /// Static constructor extracts Dal packages list and Dal type from
        /// Dal configuration file config.xml
        /// </summary>
        static DalConfing()
        {
            XElement dalConfing = XElement.Load(@"config.xml");
            DalName = dalConfing.Element("dal").Value;
            DalPackages = (from pkg in dalConfing.Element("dal-packages").Elements()
                           select pkg).ToDictionary(p => "" + p.Name, p => p.Value);
        }
        
    }
    /// <summary>
    /// Represents errors during DalApi initialization
    /// </summary>
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string message) : base(message) { }
        public DalConfigException(string message, Exception inner) : base(message, inner) { }
    }
}
