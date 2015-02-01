using DataGridDemo.Containers;
using DataGridDemo.Models;
using System.ComponentModel;
using System.Linq;

namespace DataGridDemo.ViewModels
{
    // Implement INPC just to get Assisticant to stop wrapping.
    public class GridSource : INotifyPropertyChanged
    {
        private readonly Document _document;

        private BindingListAdapter<ItemRow> _gridItems;

        public GridSource(Document document)
        {
            _document = document;

            _gridItems = new BindingListAdapter<ItemRow>(() => _document.Items
                .Select(i => new ItemRow(i)), () => new ItemRow(_document.NewItem()));
        }

        public BindingList<ItemRow> Items
        {
            get { return _gridItems.BindingList; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
