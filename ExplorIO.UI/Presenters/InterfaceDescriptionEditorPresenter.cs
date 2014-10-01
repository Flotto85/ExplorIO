using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExplorIO.UI.Views;
using ExplorIO.UI.Interfaces;
using ExplorIO.Data;

namespace ExplorIO.UI.Presenters
{
    public class InterfaceDescriptionEditorPresenter : Presenter<IInterfaceDescriptionEditorView>
    {
        #region Fields and Properties
        IoDescription interfaceDescription;

        public IoDescription InterfaceDescription
        {
            get { return this.interfaceDescription; }
            set
            {
                if (this.interfaceDescription == value)
                    return;
                this.interfaceDescription = value;
                this.InitView();
            }
        }
        #endregion

        #region Initialization
        public InterfaceDescriptionEditorPresenter(IInterfaceDescriptionEditorView view) 
            : base(view)
        {
            view.KeyDown += new System.Windows.Forms.KeyEventHandler(view_KeyDown);
        }
        #endregion

        #region Tools
        private void InitView()
        {
            View.Items = GetViewItemsFromInterfaceDescription(interfaceDescription);
            View.Update();
        }

        private IList<InterfaceDescriptionEditorItem> GetViewItemsFromInterfaceDescription(IoDescription interfaceDescription)
        {
            if (interfaceDescription == null)
                return null;
            List<InterfaceDescriptionEditorItem> items = new List<InterfaceDescriptionEditorItem>();

            int rowCounter = 0;
            while(rowCounter < interfaceDescription.Size)
            {
                InterfaceDescriptionEditorItem editorItem = new InterfaceDescriptionEditorItem();
                IoDescriptionItem descItem = interfaceDescription.GetItemAtAddress(rowCounter);
                if (descItem != null)
                {
                    editorItem.Row = descItem.Address;
                    editorItem.RowSpan = descItem.Size;
                    editorItem.Column = 0;
                    editorItem.ColSpan = 8;
                    editorItem.Text = descItem.Name;
                    editorItem.IsEmptyItem = false;
                }
                else
                {
                    editorItem.Row = rowCounter;
                    editorItem.RowSpan = 1;
                    editorItem.Column = 0;
                    editorItem.ColSpan = 8;
                }
                items.Add(editorItem);
                rowCounter += editorItem.RowSpan;
            }
            return items;
        }

        private void AddNewItemToInterfaceDescription(IList<InterfaceDescriptionEditorItem> editorItems)
        {
            int address = editorItems.Min(x => x.Row);
            int byteSize = editorItems.Sum(x=>x.RowSpan);
            IoDescriptionItem itemToAdd = new IoGroup(byteSize, address);
            itemToAdd.Name = "";
            this.interfaceDescription.AddItem(itemToAdd);
            this.InitView();
        }

        private void AddItemsToInterfaceDescriptionItem(IList<InterfaceDescriptionEditorItem> editorItems)
        {

        }
        #endregion

        #region Event Handler
        private void view_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 107:
                    IList<InterfaceDescriptionEditorItem> selectedItems = View.SelectedItems;
                    if (selectedItems.Count > 0)
                    {
                        int nonEmptyItems = selectedItems.Count(x => x.IsEmptyItem == false);
                        //Zwei oder mehr nicht leere Items zusammenzuführen wird noch nicht unterstützt
                        if (nonEmptyItems > 1) return;
                        //Wenn nur nicht leere Items markiert sind, nichts tun
                        if (selectedItems.Count == nonEmptyItems) return;
                        //Wenn keines der markierten Items leer ist, dann neues Item zur Schnittstellenbeschreibung hinzufügen
                        if (nonEmptyItems == 0)
                            AddNewItemToInterfaceDescription(selectedItems);
                        //Ansonsten item in Schnittstellenbeschreibung vergrößern
                        else
                            AddItemsToInterfaceDescriptionItem(selectedItems);
                    }
                    break;
            }
        }
        #endregion
    }
}
