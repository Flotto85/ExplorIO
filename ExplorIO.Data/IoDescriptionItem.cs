using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExplorIO.Data
{
    public abstract class IoDescriptionItem : IoData
    {
        #region Fields and Properties
        public abstract int Size { get; }
        #endregion
    }
}
