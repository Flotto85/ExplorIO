using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace ExplorIO.UI.Controls
{
    public partial class TileGridControl : UserControl
    {
        #region Fields and Properties
        TileGridItemCollection items = new TileGridItemCollection();
        SortedList<int, TileGridItem> selectedItems = new SortedList<int, TileGridItem>();
        List<string> columnHeader = new List<string>();
        int tileSize = 96;
        int columnCount = 0;
        int rowCount = 0;
        const int colHeaderHeight = 20;
        const int colHeaderSpacer = 0;
        const int rowHeaderWidth = 25;
        Color headerColor = SystemColors.Control;
        int vScrollFactor = 10;
        bool ctrlPressed;

        Bitmap itemImage;
        Bitmap colHeaderImage;
        Bitmap rowHeaderImage;

        object imageLock = new object();

        public TileGridItemCollection Items
        {
            get { return this.items; }
        }

        public List<TileGridItem> SelectedItems
        {
            get { return new List<TileGridItem>(selectedItems.Values); }
        }

        public int ColumnCount
        {
            get { return this.columnCount; }
            set {
                if (columnCount != value)
                {
                    columnCount = value;

                    for (int i = columnHeader.Count; i <= columnCount; i++)
                    {
                        columnHeader.Add("");
                    }
                    this.UpdateControl();
                }
            }
        }

        public List<string> ColumnHeader
        {
            get { return this.columnHeader; }
        }

        public int TileSize
        {
            get { return this.tileSize; }
            set
            {
                if (this.tileSize != value)
                {
                    this.tileSize = value;
                    this.UpdateControl();
                }
            }
        }

        public int RowCount
        {
            get { return this.rowCount; }
        }
        #endregion

        #region Initialization
        public TileGridControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);

            this.vScrollBar.Minimum = 0;
            this.vScrollBar.Value = this.vScrollBar.Minimum;
            this.vScrollBar.Maximum = tileSize;
            this.vScrollBar.SmallChange = 1;
            this.vScrollBar.LargeChange = tileSize;

            items.ItemsAdded += new EventHandler<GridItemCollectionChangedEventArgs>(items_ItemsAdded);
            items.ItemsRemoved += new EventHandler<GridItemCollectionChangedEventArgs>(items_ItemsRemoved);
            items.CollectionCleared += new EventHandler<GridItemCollectionChangedEventArgs>(items_CollectionCleared);

            this.vScrollBar.ValueChanged += new EventHandler(vScrollBar_ValueChanged);

            this.MouseDown += new MouseEventHandler(TileGridControl_MouseDown);
            this.KeyDown += new KeyEventHandler(TileGridControl_KeyDown);
            this.KeyUp += new KeyEventHandler(TileGridControl_KeyUp);

            this.LostFocus += new EventHandler(TileGridControl_LostFocus);

            ctrlPressed = false;
        }
        #endregion

        #region EventHandler
        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            this.Invalidate();
            this.Update();
        }

        private void items_CollectionCleared(object sender, GridItemCollectionChangedEventArgs e)
        {
            foreach (TileGridItem item in e.ChangedItems)
            {
                this.selectedItems.Clear();
                item.PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
                this.rowCount = 0;
            }
            UpdateControl();
        }

        private void items_ItemsRemoved(object sender, GridItemCollectionChangedEventArgs e)
        {
            foreach (TileGridItem item in e.ChangedItems)
            {
                if (this.selectedItems.ContainsKey(item.Row))
                    this.selectedItems.Remove(item.Row);
                item.PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
                this.rowCount = this.items.Max(x => x.Row) + 1;
            }
            UpdateControl();
        }

        private void items_ItemsAdded(object sender, GridItemCollectionChangedEventArgs e)
        {
            foreach (TileGridItem item in e.ChangedItems)
            {
                item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                this.rowCount = this.items.Max(x => x.Row) + 1;
            }
            UpdateControl();
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ("IsSelected"):
                    this.ReDrawItem((TileGridItem)sender as TileGridItem);
                    this.Invalidate();
                    this.Update();
                    break;
            }
        }

        private void TileGridControl_MouseDown(object sender, MouseEventArgs e)
        {
            //this.Focus();
            if (!this.ctrlPressed)
            {
                foreach (TileGridItem selItem in this.selectedItems.Values)
                    selItem.IsSelected = false;
                selectedItems.Clear();
            }
            double scaledValue = GetScaledScrollBarValue();
            Point worldCoordinates = new Point(e.Location.X - rowHeaderWidth, e.Location.Y + (int)(scaledValue * (double)this.vScrollFactor) - (colHeaderHeight + colHeaderSpacer));
            if (worldCoordinates.X < 0 || worldCoordinates.Y < 0)
                return;
            Point tileCoordinates = new Point(worldCoordinates.X / tileSize, worldCoordinates.Y / tileSize);
            TileGridItem clickedItem = this.GetItemAtTilePos(tileCoordinates);
            if (clickedItem != null)
            {
                clickedItem.IsSelected = !clickedItem.IsSelected;
                if (clickedItem.IsSelected)
                {
                    this.selectedItems.Add(clickedItem.Row, clickedItem);
                }
                else
                {
                    this.selectedItems.Remove(clickedItem.Row);
                }
            }
        }

        private void TileGridControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (17): // Ctrl
                    this.ctrlPressed = true;
                    break;
            }
        }

        private void TileGridControl_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (17): // Ctrl
                    this.ctrlPressed = false;
                    break;
            }
        }

        void TileGridControl_LostFocus(object sender, EventArgs e)
        {
            this.ctrlPressed = false;
        }
        #endregion

        #region Interface
        #endregion

        #region Tools
        private void UpdateControl()
        {
            CreateColHeaderImage();
            CreateRowHeaderImage();
            CreateItemImage();
            this.Invalidate();
            this.Update();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Gesamthöhe berechnen
            int totalheight = rowCount * tileSize;// +colHeaderHeight + colHeaderSpacer;
            this.vScrollBar.Visible = totalheight > this.Height;

            if (this.vScrollBar.Visible == false && this.vScrollBar.Value != this.vScrollBar.Minimum)
                this.vScrollBar.Value = this.vScrollBar.Minimum;
            int scrollMax =  ((totalheight - this.Height + tileSize) / vScrollFactor);
            if (scrollMax < vScrollBar.LargeChange) 
                scrollMax = vScrollBar.LargeChange;
            this.vScrollBar.Maximum = scrollMax;
            double scaledValue = GetScaledScrollBarValue();
            //(this.vScrollBar.Maximum + 1 - this.vScrollBar.LargeChange)
            int yOffset = (int)(scaledValue * (double)vScrollFactor);

            if (this.itemImage != null && this.colHeaderImage != null && this.rowHeaderImage != null)
            {
                //if (yOffset > totalheight - (this.Height - colHeaderHeight - colHeaderSpacer))
                //    yOffset = totalheight - (this.Height - colHeaderHeight - colHeaderSpacer);
                if (yOffset < 0)
                    yOffset = 0;
                int width = (itemImage.Width >= (this.Width - rowHeaderWidth)) ? (this.Width - rowHeaderWidth) : itemImage.Width;
                int height = (itemImage.Height >= (this.Height - (colHeaderSpacer + colHeaderHeight))) ? (this.Height - colHeaderSpacer + colHeaderHeight) : itemImage.Height;
                if (width > 0 && height > 0)
                {
                    Rectangle itemSrcRect = new Rectangle(0, yOffset, width, height);
                    Rectangle rowHeaderSrcRect = new Rectangle(0, yOffset, rowHeaderImage.Width, height);
                    e.Graphics.DrawImage(itemImage, rowHeaderWidth, colHeaderSpacer + colHeaderHeight, itemSrcRect, GraphicsUnit.Pixel);
                    e.Graphics.DrawImage(colHeaderImage, rowHeaderWidth, 0);
                    e.Graphics.DrawImage(rowHeaderImage, 0, colHeaderSpacer + colHeaderHeight, rowHeaderSrcRect, GraphicsUnit.Pixel);
                }
            }

            //e.Graphics.DrawImage(this.itemImage, new Point(0, 0));
        }

        private void CreateItemImage()
        {
            if (columnCount <= 0 || rowCount <= 0 || tileSize <= 0)
                return;
            lock (imageLock)
            {
                if (itemImage != null)
                {
                    itemImage.Dispose();
                }

                itemImage = new Bitmap(this.columnCount * this.tileSize, this.rowCount * this.tileSize);

                using (Graphics imageGraphics = Graphics.FromImage(itemImage))
                {
                    imageGraphics.Clear(this.BackColor);
                    if (this.items == null || this.rowCount == 0)
                        return;

                    imageGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                    foreach (TileGridItem item in this.items)
                    {
                        DrawItem(imageGraphics, item, new Point(0, 0));
                    }
                    imageGraphics.Flush();
                }
            }
        }

        private void DrawItem(Graphics graphics, TileGridItem item, Point offset)
        {
            Rectangle tile = new Rectangle(item.Col * tileSize + offset.X, item.Row * tileSize + offset.Y, item.ColSpan * tileSize, item.RowSpan * tileSize);
            Color tileColor = item.IsSelected ? item.SelectedColor : item.BackColor;
            SolidBrush tileBrush = new SolidBrush(tileColor);

            graphics.FillRectangle(tileBrush, tile);
            tileBrush.Color = item.ForeColor;
            graphics.DrawLine(new Pen(tileBrush), new Point(tile.Left, tile.Bottom - 1), new Point(tile.Right - 1, tile.Bottom - 1));
            graphics.DrawString(item.Text, this.Font, tileBrush, new PointF(10f + tile.X, 10f + tile.Y));
        }

        private void ReDrawItem(TileGridItem item)
        {
            if (itemImage == null)
                return;
            using (Graphics graphics = Graphics.FromImage(itemImage))
            {
                DrawItem(graphics, item, new Point(0, 0));
            }
        }

        private void CreateColHeaderImage()
        {
            if (columnCount <= 0)
                return;
            this.colHeaderImage = new Bitmap((columnCount * tileSize) + 1, colHeaderHeight);
            using (Graphics graphics = Graphics.FromImage(colHeaderImage))
            {
                Pen blackPen = new Pen(Color.Black);
                SolidBrush backBrush = new SolidBrush(headerColor);
                SolidBrush textBrush = new SolidBrush(this.ForeColor);
                Rectangle rect;
                for (int i = 0; i < columnCount; i++)
                {
                    rect = new Rectangle((tileSize * i), 0, tileSize, colHeaderHeight);
                    graphics.FillRectangle(backBrush, rect);
                    graphics.DrawLine(blackPen, new Point(rect.Left, rect.Top), new Point(rect.Left, rect.Bottom - 1));
                    graphics.DrawLine(blackPen, new Point(rect.Right, rect.Top), new Point(rect.Right, rect.Bottom - 1));
                    SizeF stringSize = graphics.MeasureString(columnHeader[i], this.Font);
                    graphics.DrawString(columnHeader[i], this.Font, textBrush, new PointF(rect.Left + ((rect.Width - stringSize.Width) / 2), (rect.Height - stringSize.Height) / 2));
                }
                graphics.DrawLine(blackPen, new Point(0, 0), new Point((tileSize * columnCount), 0));
                graphics.DrawLine(blackPen, new Point(0, colHeaderHeight - 1), new Point((tileSize * columnCount), colHeaderHeight - 1));
            }
        }

        private void CreateRowHeaderImage()
        {
            if (rowCount <= 0)
                return;
            if (this.rowHeaderImage != null)
                this.rowHeaderImage.Dispose();
            this.rowHeaderImage = new Bitmap(rowHeaderWidth + 1, (rowCount * tileSize) + 1);
            using (Graphics graphics = Graphics.FromImage(rowHeaderImage))
            {
                Pen blackPen = new Pen(Color.Black);
                SolidBrush backBrush = new SolidBrush(headerColor);
                SolidBrush textBrush = new SolidBrush(this.ForeColor);
                Rectangle rect;
                for (int i = 0; i < rowCount; i++)
                {
                    rect = new Rectangle(0, tileSize * i, rowHeaderWidth, tileSize);
                    graphics.FillRectangle(backBrush, rect);
                    graphics.DrawLine(blackPen, new Point(rect.Left, rect.Top - 1), new Point(rect.Right, rect.Top - 1));
                    graphics.DrawLine(blackPen, new Point(rect.Left, rect.Bottom - 1), new Point(rect.Right, rect.Bottom - 1));
                    string rowTxt = (i + 1).ToString();
                    SizeF stringSize = graphics.MeasureString(rowTxt, this.Font);
                    graphics.DrawString(rowTxt, this.Font, textBrush, new PointF(((rect.Width - stringSize.Width) / 2), rect.Top + (rect.Height - stringSize.Height) / 2));
                }
                graphics.DrawLine(blackPen, new Point(0, 0), new Point(0, tileSize * rowCount));
                graphics.DrawLine(blackPen, new Point(rowHeaderWidth, 0), new Point(rowHeaderWidth, tileSize * rowCount));
            }
        }

        private TileGridItem GetItemAtTilePos(Point pos)
        {
            if (this.items == null || this.items.Count == 0)
                return null;
            return this.items.FirstOrDefault(x => pos.Y <= x.Row + x.RowSpan - 1 && pos.Y >= x.Row && pos.X <= x.Col + x.ColSpan - 1 && pos.X >= x.Col);
            //return this.items.FirstOrDefault(x => pos.Y <= x.Row + x.RowSpan - 1 && pos.Y >= x.Row);
        }

        private double GetScaledScrollBarValue()
        {
            double factor = (double)vScrollBar.Value / (double)((this.vScrollBar.Maximum + 1 - this.vScrollBar.LargeChange) - vScrollBar.Minimum);
            double scaledValue = (int)(factor * (double)this.vScrollBar.Maximum);
            return scaledValue;
        }
        #endregion 
    }

    public class TileGridItem : INotifyPropertyChanged
    {
        #region Fields and Properties
        private int widthInUnits = 1;
        private int heightInUnits = 1;
        private string text = "IoInterfaceGridItem";
        private Color backColor = Color.White;
        private Color foreColor = Color.Black;
        private Color selectedColor = Color.LightBlue;
        private int row = 1;
        private int col = 1;
        private bool isSelected = false;

        public int ColSpan {
            get { return this.widthInUnits; }
            set
            {
                if (this.widthInUnits == value)
                    return;
                if (value <= 1)
                    this.widthInUnits = 1;
                else
                    this.widthInUnits = value;
                this.PerformPropertyChanged("WidthInUnits");
            }
        }

        public int RowSpan {
            get { return this.heightInUnits; }
            set
            {
                if (this.heightInUnits == value)
                    return;
                if (value <= 1)
                    this.heightInUnits = 1;
                else
                    this.heightInUnits = value;
                this.PerformPropertyChanged("HeightInUnits");
            }
        }

        public string Text {
            get { return this.text; }
            set
            {
                if (this.text == value)
                    return;
                this.text = value;
                this.PerformPropertyChanged("Text");
            }
        }

        public Color BackColor {
            get { return this.backColor; }
            set
            {
                if (this.backColor == value)
                    return;
                this.backColor = value;
                this.PerformPropertyChanged("BackColor");
            }
        }
        public Color ForeColor
        {
            get { return this.foreColor; }
            set
            {
                if (this.foreColor == value)
                    return;
                this.foreColor = value;
                this.PerformPropertyChanged("ForeColor");
            }
        }
        public Color SelectedColor
        {
            get { return this.selectedColor; }
            set
            {
                if (this.selectedColor == value)
                    return;
                this.selectedColor = value;
                this.PerformPropertyChanged("SelectedColor");
            }
        }

        public object Tag { get; set; }

        public int Row
        {
            get { return this.row; }
            set
            {
                if (this.row == value)
                    return;
                if (value < 0)
                    this.row = 0;
                else
                    this.row = value;
                this.PerformPropertyChanged("Row");
            }
        }
        public int Col
        {
            get { return this.col; }
            set
            {
                if (this.col == value)
                    return;
                if (value < 0)
                    this.col = 0;
                else
                    this.col = value;
                this.PerformPropertyChanged("Col");
            }
        }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (this.isSelected == value)
                    return;
                this.isSelected = value;
                this.PerformPropertyChanged("IsSelected");
            }
        }
        #endregion

        #region Initialization
        public TileGridItem()
        {

        }

        public TileGridItem(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Inteface
        public override string ToString()
        {
            return String.IsNullOrEmpty(text) ? "" : text;
        }
        #endregion

        #region Tools
        private void PerformPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null){
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, e);
            }
        }

        #endregion
    }

    public class TileGridItemCollection : ICollection<TileGridItem>
    {
        #region Fields and Properties
        SortedList<int, TileGridItem> innerList;

        public TileGridItem this[int row, int col]
        {
            get
            {
                int index = row * 1000 + col;
                return this.innerList[index];
            }
            set
            {
                int index = row * 1000 + col;
                this.innerList[index] = value;
            }
        }
        #endregion

        #region Initialization
        public TileGridItemCollection()
        {
            this.innerList = new SortedList<int, TileGridItem>();
        }

        public TileGridItemCollection(IEnumerable<TileGridItem> items)
            : this()
        {
            this.AddRange(items);
        }
        #endregion

        #region Events
        public event EventHandler<GridItemCollectionChangedEventArgs> ItemsAdded;
        public event EventHandler<GridItemCollectionChangedEventArgs> ItemsRemoved;
        public event EventHandler<GridItemCollectionChangedEventArgs> CollectionCleared;
        #endregion

        #region Interface
        public void Add(TileGridItem item)
        {
            if (item == null)
                throw new ArgumentNullException("Item cannot be nothing.");
            int row = 0;
            if (this.innerList.Count > 0)
                row = this.innerList.Values[this.innerList.Count - 1].Row + this.innerList.Values[this.innerList.Count - 1].RowSpan - 1;
            int index = item.Row * 1000 + item.Col;
            this.innerList.Add(index, item);
            this.PerformItemsAdded(item);
        }

        public void AddRange(IEnumerable<TileGridItem> items)
        {
            if (items == null)
                throw new ArgumentNullException("Items cannot be nothing.");
            foreach (TileGridItem item in items)
            {
                int row = 0;
                if (this.innerList.Count > 0)
                    row = this.innerList.Values[this.innerList.Count - 1].Row + this.innerList.Values[this.innerList.Count - 1].RowSpan;
                //item.Row = row;
                int index = item.Row * 1000 + item.Col;
                this.innerList.Add(index, item);
            }
            this.PerformItemsAdded(items);
        }

        public void Clear()
        {
            TileGridItem[] items = innerList.Values.ToArray();
            this.innerList.Clear();
            this.PerformItemsCleared(items);
        }

        public bool Contains(TileGridItem item)
        {
            return (this.innerList.Values.Contains(item));
        }

        public int Count
        {
            get { return this.innerList.Count; }
        }

        public bool Remove(TileGridItem item)
        {
            if (this.innerList.Remove(item.Row))
            {
                this.PerformItemsRemoved(item);
                return true;
            }
            else
                return false;
        }

        public System.Collections.Generic.IEnumerator<TileGridItem> GetEnumerator()
        {
            return this.innerList.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void CopyTo(TileGridItem[] array, int arrayIndex)
        {
            this.innerList.Values.CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
        #endregion

        #region Tools
        private void PerformItemsAdded(IEnumerable<TileGridItem> items)
        {
            if (this.ItemsAdded != null)
            {
                this.ItemsAdded(this, GetGridItemCollectionChangedEventArgs(items));
            }
        }

        private void PerformItemsAdded(TileGridItem item)
        {
            if (this.ItemsAdded != null)
            {
                this.ItemsAdded(this, GetGridItemCollectionChangedEventArgs(item));
            }
        }

        private void PerformItemsRemoved(IEnumerable<TileGridItem> items)
        {
            if (this.ItemsRemoved != null)
            {
                this.ItemsRemoved(this, GetGridItemCollectionChangedEventArgs(items));
            }
        }

        private void PerformItemsRemoved(TileGridItem item)
        {
            if (this.ItemsRemoved != null)
            {
                this.ItemsRemoved(this, GetGridItemCollectionChangedEventArgs(item));
            }
        }

        private void PerformItemsCleared(IEnumerable<TileGridItem> items)
        {
            if (this.CollectionCleared != null)
            {
                this.CollectionCleared(this, GetGridItemCollectionChangedEventArgs(items));
            }
        }

        private GridItemCollectionChangedEventArgs GetGridItemCollectionChangedEventArgs(IEnumerable<TileGridItem> items)
        {
            GridItemCollectionChangedEventArgs e = new GridItemCollectionChangedEventArgs();
            e.ChangedItems = items;
            return e;
        }

        private GridItemCollectionChangedEventArgs GetGridItemCollectionChangedEventArgs(TileGridItem item)
        {
            return GetGridItemCollectionChangedEventArgs(new List<TileGridItem>() { item });
        }
        #endregion
    }

    public class GridItemCollectionChangedEventArgs : EventArgs
    {
        public IEnumerable<TileGridItem> ChangedItems { get; set; }
    }
}
