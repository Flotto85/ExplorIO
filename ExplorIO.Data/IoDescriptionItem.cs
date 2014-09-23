using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ExplorIO.Data
{
    [DataContract]
    public abstract class IoDescriptionItem : IoData
    {
        #region Fields and Properties
        public abstract int Size { get; }
        public int Address { get; internal set; }
        #endregion
    }
}
