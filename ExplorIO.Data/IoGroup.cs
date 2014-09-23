using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ExplorIO.Data
{
    [DataContract]
    public class IoGroup : IoDescriptionItem
    {
        #region Fields and Properties
        private int size;

        public override int Size {
            get { return this.size; }
        }
      
        #endregion

        #region Initialization
        public IoGroup(int address)
        {
            this.size = 1;
            this.Address = address;
        }

        public IoGroup(int size, int address)
        {
            this.Address = address;
            this.size = size;
        }
        #endregion

        #region Interface
        #endregion
    }
}
