using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExplorIO.Data
{
    public class IoGroup : IoDescriptionItem
    {
        #region Fields and Properties
        private int size;

        public override int Size {
            get { return this.size; }
        }
      
        #endregion

        #region Initialization
        public IoGroup()
        {
            this.size = 1;
        }

        public IoGroup(int size)
        {
            this.size = size;
        }
        #endregion

        #region Interface
        #endregion
    }
}
