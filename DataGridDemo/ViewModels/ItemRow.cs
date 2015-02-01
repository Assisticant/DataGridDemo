using DataGridDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataGridDemo.ViewModels
{
    public class ItemRow : IEditableObject
    {
        private readonly Item _item;

        public ItemRow(Item item)
        {
            _item = item;            
        }

        public string Name
        {
            get { return _item.Name; }
            set { _item.Name = value; }
        }

        public void BeginEdit()
        {
            throw new NotImplementedException();
        }

        public void CancelEdit()
        {
            throw new NotImplementedException();
        }

        public void EndEdit()
        {
            throw new NotImplementedException();
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
