using Assisticant.Fields;

namespace DataGridDemo.Models
{
    public class Item
    {
        private Observable<string> _name = new Observable<string>();

        public string Name
        {
            get => _name;
            set => _name.Value = value;
        }
    }
}
