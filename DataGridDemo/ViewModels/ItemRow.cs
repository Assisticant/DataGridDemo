using Assisticant.Fields;
using DataGridDemo.Models;
using System;
using System.ComponentModel;

namespace DataGridDemo.ViewModels
{
    public class ItemRow : IEditableObject
    {
        private readonly Item _item;

        private Observable<bool> _editing = new Observable<bool>(false);
        private Observable<string> _name = new Observable<string>();

        public ItemRow(Item item)
        {
            _item = item;            
        }

        public string Name
        {
            get
            {
                if (_editing.Value)
                    return _name.Value;
                else
                    return _item.Name;
            }
            set
            {
                if (_editing.Value)
                    _name.Value = value;
                else
                    _item.Name = value;
            }
        }

        public void BeginEdit()
        {
            _editing.Value = true;
            _name.Value = _item.Name;
        }

        public void CancelEdit()
        {
            _editing.Value = false;
        }

        public void EndEdit()
        {
            _editing.Value = false;
            _item.Name = _name.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ItemRow that = (ItemRow)obj;
            return Object.Equals(this._item, that._item);
        }

        public override int GetHashCode()
        {
            return _item.GetHashCode();
        }
    }
}
