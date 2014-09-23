using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExplorIO.Data
{
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
