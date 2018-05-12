using Assisticant.Fields;

namespace DataGridDemo.Models
{
    public class Selection
    {
        private Observable<Item> _selectedItem = new Observable<Item>();

        public Item SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem.Value = value;
        }
    }
}
