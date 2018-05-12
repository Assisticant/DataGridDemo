using DataGridDemo.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataGridDemo.ViewModels
{
    public class MainViewModel
    {
        private readonly Document _document;
        private readonly Selection _selection;

        public MainViewModel(Document document, Selection selection)
        {
            _document = document;
			_selection = selection;
        }

        public IEnumerable<ItemRow> Rows =>
            from item in _document.Items
            select new ItemRow(item);

        public ItemRow NewItemInRows()
        {
            return new ItemRow(_document.NewItem());
        }

        public void DeleteItemFromRows(ItemRow i)
        {
            _document.DeleteItem(i.Item);
        }

        public IEnumerable<ItemHeader> Items =>
            from item in _document.Items
            select new ItemHeader(item);

        public ItemHeader SelectedItem
        {
            get => _selection.SelectedItem == null
                    ? null
                    : new ItemHeader(_selection.SelectedItem);
            set => _selection.SelectedItem = value?.Item;
        }

        public ItemViewModel ItemDetail =>
            _selection.SelectedItem == null
                    ? null
                    : new ItemViewModel(_selection.SelectedItem);

        public void AddItem()
        {
            _selection.SelectedItem = _document.NewItem();
        }

        public bool CanDeleteItem => _selection.SelectedItem != null;

        public void DeleteItem()
        {
            _document.DeleteItem(_selection.SelectedItem);
            _selection.SelectedItem = null;
        }

        public bool CanMoveItemDown =>
            _selection.SelectedItem != null &&
            _document.CanMoveDown(_selection.SelectedItem);

        public void MoveItemDown()
        {
            _document.MoveDown(_selection.SelectedItem);
        }

        public bool CanMoveItemUp =>
            _selection.SelectedItem != null &&
            _document.CanMoveUp(_selection.SelectedItem);

        public void MoveItemUp()
        {
            _document.MoveUp(_selection.SelectedItem);
        }
    }
}
