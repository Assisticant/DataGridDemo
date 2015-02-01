using Assisticant;
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DataGridDemo.Containers
{
    public class BindingListAdapter<T>
        where T: class
    {
        private readonly Func<T> _factory;
        private readonly Action<int, T> _insert;
        private readonly Action<int> _remove;

        private BindingList<object> _bindingList;
        private ComputedSubscription _subscription;
        private bool _updating = false;
        
        public BindingListAdapter(
            Func<IEnumerable<T>> getItems,
            Func<T> factory,
            Action<int, T> insert,
            Action<int> remove)
        {
            _factory = factory;
            _insert = insert;
            _remove = remove;

            _bindingList = new BindingList<object>();
            _bindingList.AddingNew += AddingNewItem;
            _bindingList.ListChanged += ItemListChanged;
            _bindingList.AllowNew = true;
            _bindingList.AllowEdit = true;
            _bindingList.AllowRemove = true;

            _subscription = new Computed<List<T>>(() => getItems().ToList())
                .Subscribe(UpdateBindingList);
        }

        public BindingList<object> BindingList
        {
            get { return _bindingList; }
        }

        private void AddingNewItem(object sender, AddingNewEventArgs e)
        {
            e.NewObject = ForView.Wrap(_factory());
        }

        private void ItemListChanged(object sender, ListChangedEventArgs e)
        {
            if (!_updating)
            {
                var scheduler = UpdateScheduler.Begin();

                try
                {
                    if (e.ListChangedType == ListChangedType.ItemAdded)
                    {
                        int index = e.NewIndex;
                        T item = ForView.Unwrap<T>(_bindingList[index]);
                        _insert(index, item);
                    }
                    else if (e.ListChangedType == ListChangedType.ItemDeleted)
                    {
                        int index = e.NewIndex;
                        _remove(index);
                    }
                }
                finally
                {
                    var updates = scheduler.End();
                    foreach (var update in updates)
                    {
                        update();
                    }
                }
            }
        }

        private void UpdateBindingList(List<T> items)
        {
            _updating = true;

            try
            {
                var _itemContainers = new List<ItemContainer<T>>();
                using (var bin = new RecycleBin<ItemContainer<T>>(_bindingList.Select(obj => new ItemContainer<T>(ForView.Unwrap<T>(obj), _bindingList, true))))
                {
                    foreach (var item in items)
                    {
                        var itemContainer = bin.Extract(new ItemContainer<T>(item, _bindingList, false));
                        _itemContainers.Add(itemContainer);
                    }
                }

                int index = 0;
                foreach (var itemContainer in _itemContainers)
                {
                    itemContainer.EnsureInCollection(index);
                    index++;
                }
            }
            finally
            {
                _updating = false;
            }
        }
    }
}
