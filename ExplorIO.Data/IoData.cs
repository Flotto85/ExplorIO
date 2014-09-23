using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ExplorIO.Data
{
    [DataContract]
    public abstract class IoData
    {
        #region Fields and Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public string InVariableName { get; set; }
        public string OutVariableName { get; set; }
        #endregion
    }
}
