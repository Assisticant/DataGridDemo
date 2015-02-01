using System;
using System.Linq;
using DataGridDemo.Models;

namespace DataGridDemo.ViewModels
{
    public class ItemHeader
    {
        private readonly Item _item;

        public ItemHeader(Item item)
        {
            _item = item;
        }

        public Item Item
        {
            get { return _item; }
        }

        public string Name
        {
            get { return _item.Name ?? "<New Item>"; }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            ItemHeader that = obj as ItemHeader;
            if (that == null)
                return false;
            return Object.Equals(this._item, that._item);
        }

        public override int GetHashCode()
        {
            return _item.GetHashCode();
        }
    }
}
