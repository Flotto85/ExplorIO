using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExplorIO.UI.Interfaces;
using ExplorIO.UI.Controls;

namespace ExplorIO.UI.Views
{
    public partial class InterfaceDescriptionEditorView : UserControl, IInterfaceDescriptionEditorView
    {
        #region Fields and Properties
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<InterfaceDescriptionEditorItem> Items
        {
            get
            {
                return EditorItemsFromTileItems(this.tileGridControl.Items);
            }
            set
            {
                this.tileGridControl.Items.Clear();
                List<TileGridItem> items = TileItemsFromEditorItems(value);
                this.tileGridControl.Items.AddRange(items);
            }
        }

        public IList<InterfaceDescriptionEditorItem> SelectedItems
        {
            get { return EditorItemsFromTileItems(this.tileGridControl.SelectedItems); }
        }

        public Color EmptyItemColor { get; set; }
        #endregion

        #region Events
        public new event KeyEventHandler KeyDown;
        #endregion

        #region Initialization
        public InterfaceDescriptionEditorView()
        {
            InitializeComponent();
            this.tileGridControl.KeyDown += new KeyEventHandler(tileGridControl_KeyDown);
            this.EmptyItemColor = Color.MediumPurple;
            this.tileGridControl.ColumnCount = 8;
            for (int i = 0; i < 8; i++)
            {
                this.tileGridControl.ColumnHeader[i] = "Bit " + i.ToString();
            }
        }
        #endregion

        #region Interface
        public new void Update()
        {

        }
        #endregion

        #region Tools
        private List<InterfaceDescriptionEditorItem> EditorItemsFromTileItems(IEnumerable<TileGridItem> tileItems)
        {
            List<InterfaceDescriptionEditorItem> items = new List<InterfaceDescriptionEditorItem>();
            foreach (var item in tileItems)
            {
                InterfaceDescriptionEditorItem editorItem = new InterfaceDescriptionEditorItem();
                editorItem.Row = item.Row;
                editorItem.Column = item.Col;
                editorItem.RowSpan = item.RowSpan;
                editorItem.ColSpan = item.ColSpan;
                editorItem.Text = item.Text;
                editorItem.IsEmptyItem = item.Tag == null;
                items.Add(editorItem);
            }
            return items;
        }

        private List<TileGridItem> TileItemsFromEditorItems(IList<InterfaceDescriptionEditorItem> editorItems)
        {
            List<TileGridItem> tileItems = new List<TileGridItem>();
            foreach (var item in editorItems)
            {
                TileGridItem tileItem = new TileGridItem();
                tileItem.Row = item.Row;
                tileItem.Col = item.Column;
                tileItem.RowSpan = item.RowSpan;
                tileItem.ColSpan = item.ColSpan;
                tileItem.Text = item.Text;
                if (item.IsEmptyItem)
                    tileItem.BackColor = EmptyItemColor;
                else
                    tileItem.Tag = new object();
                tileItems.Add(tileItem);
            }
            return tileItems;
        }
        #endregion

        #region EventHandler
        private void tileGridControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.KeyDown != null)
                this.KeyDown(this, e);
        }
        #endregion
    }
}
