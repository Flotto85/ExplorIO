using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExplorIO.UI.Interfaces;
using System.Drawing;
using System.Windows.Forms;

namespace ExplorIO.UI.Views
{
    public interface IInterfaceDescriptionEditorView : IView
    {
        IList<InterfaceDescriptionEditorItem> Items { get; set; }
        IList<InterfaceDescriptionEditorItem> SelectedItems { get; }
        Color EmptyItemColor { get; set; }
        event KeyEventHandler KeyDown;
        void Update();
    }

    [Serializable]
    public class InterfaceDescriptionEditorItem
    {
        #region Fields and Properties
        public int Column { get; set; }
        public int Row { get; set; }
        public int ColSpan { get; set; }
        public int RowSpan { get; set; }
        public string Text { get; set; }
        public bool IsEmptyItem { get; set; }
        #endregion

        #region Initialization 
        public InterfaceDescriptionEditorItem()
        {
            Column = 0;
            Row = 0;
            ColSpan = 1;
            RowSpan = 1;
            Text = "Reserve";
            IsEmptyItem = true;
        }

        public InterfaceDescriptionEditorItem(int row, int col)
            : this()
        {
            this.Row = row;
            this.Column = col;
        }

        public InterfaceDescriptionEditorItem(int row, int col, int rowSpan, int colSpan)
            : this(row, col)
        {
            this.RowSpan = rowSpan;
            this.ColSpan = colSpan;
        }

        public InterfaceDescriptionEditorItem(int row, int col, int rowSpan, int colSpan, string text)
            : this(row, col, rowSpan, colSpan)
        {
            this.Text = text;
        }

        public InterfaceDescriptionEditorItem(int row, int col, int rowSpan, int colSpan, string text, bool isEmptyItem)
            : this(row, col, rowSpan, colSpan, text)
        {
            this.IsEmptyItem = isEmptyItem;
        }
        #endregion 
    }
}
