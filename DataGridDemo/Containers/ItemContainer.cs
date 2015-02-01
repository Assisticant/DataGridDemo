using System;
using System.ComponentModel;

namespace DataGridDemo.Containers
{
    class ItemContainer<T> : IDisposable
    {
        private readonly T _item;
        private readonly BindingList<T> _bindingList;

        private bool _inCollection = false;

        public ItemContainer(T item, BindingList<T> bindingList)
        {
            _item = item;
            _bindingList = bindingList;
        }

        public T Item
        {
            get { return _item; }
        }

        public void EnsureInCollection(int index)
        {
            if (!_inCollection)
            {
                _bindingList.Insert(index, _item);
                _inCollection = true;
            }
            else if (!Object.Equals(_bindingList[index], _item))
            {
                _bindingList.Remove(_item);
                _bindingList.Insert(index, _item);
            }
        }

        public void Dispose()
        {
            if (_inCollection)
            {
                _bindingList.Remove(_item);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            var that = obj as ItemContainer<T>;
            if (that == null)
                return false;
            return Object.Equals(_item, that._item);
        }

        public override int GetHashCode()
        {
            return _item.GetHashCode();
        }
    }
}
