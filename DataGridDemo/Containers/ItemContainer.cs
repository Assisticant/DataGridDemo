using Assisticant;
using System;
using System.ComponentModel;

namespace DataGridDemo.Containers
{
    class ItemContainer<T> : IDisposable
    {
        private readonly T _item;
        private readonly BindingList<object> _bindingList;
        private readonly bool _inCollection;

        public ItemContainer(T item, BindingList<object> bindingList, bool inCollection)
        {
            _item = item;
            _bindingList = bindingList;
            _inCollection = inCollection;
        }

        public T Item
        {
            get { return _item; }
        }

        public void EnsureInCollection(int index)
        {
            object wrapper = ForView.Wrap(_item);
            if (!_inCollection)
            {
                _bindingList.Insert(index, wrapper);
            }
            else if (!Object.Equals(_bindingList[index], wrapper))
            {
                _bindingList.Remove(wrapper);
                _bindingList.Insert(index, wrapper);
            }
        }

        public void Dispose()
        {
            if (_inCollection)
            {
                _bindingList.Remove(ForView.Wrap(_item));
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
