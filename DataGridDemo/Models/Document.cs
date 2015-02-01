using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant.Collections;
using Assisticant.Fields;

namespace DataGridDemo.Models
{
    public class Document
    {
		private Observable<string> _name = new Observable<string>();
        private ObservableList<Item> _items = new ObservableList<Item>();

		public string Name
		{
			get { return _name; }
			set { _name.Value = value; }
		}

        public IEnumerable<Item> Items
        {
            get { return _items; }
        }

        public Item NewItem()
        {
            Item item = new Item();
            _items.Add(item);
            return item;
        }

        public void DeleteItem(Item item)
        {
            _items.Remove(item);
        }

        public bool CanMoveDown(Item item)
        {
            return _items.IndexOf(item) < _items.Count - 1;
        }

        public bool CanMoveUp(Item item)
        {
            return _items.IndexOf(item) > 0;
        }

        public void MoveDown(Item item)
        {
            int index = _items.IndexOf(item);
            _items.RemoveAt(index);
            _items.Insert(index + 1, item);
        }

        public void MoveUp(Item item)
        {
            int index = _items.IndexOf(item);
            _items.RemoveAt(index);
            _items.Insert(index - 1, item);
        }
    }
}
