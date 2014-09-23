using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExplorIO.Data
{
    public class IoByte : IoDescriptionItem
    {
        #region Fields and Properties
        private IoBit[] bits;


        public IoBit[] Bits
        {
            get { return (IoBit[])this.bits.Clone(); }
        }

        public override int Size
        {
            get { return 1; }
        }
        #endregion

        #region Initialization
        public IoByte()
        {
            this.bits = new IoBit[8];
        }
        #endregion
    }
}
