using Assisticant;
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DataGridDemo.Containers
{
    public class BindingListAdapter<T>
    {
        private readonly Func<T> _factory;
        private readonly Action<int, T> _insert;

        private BindingList<T> _bindingList;
        private ComputedSubscription _subscription;
        private bool _updating = false;
        
        public BindingListAdapter(Func<IEnumerable<T>> getItems, Func<T> factory, Action<int, T> insert)
        {
            _factory = factory;
            _insert = insert;

            _bindingList = new BindingList<T>();
            _bindingList.AddingNew += AddingNewItem;
            _bindingList.ListChanged += ItemListChanged;
            _bindingList.AllowNew = true;
            _bindingList.AllowEdit = true;
            _bindingList.AllowRemove = true;

            _subscription = new Computed<List<T>>(() => getItems().ToList())
                .Subscribe(UpdateBindingList);
        }

        public BindingList<T> BindingList
        {
            get { return _bindingList; }
        }

        private void AddingNewItem(object sender, AddingNewEventArgs e)
        {
            e.NewObject = _factory();
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
                        T item = _bindingList[index];
                        _insert(index, item);
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
                using (var bin = new RecycleBin<ItemContainer<T>>(_bindingList.Select(i => new ItemContainer<T>(i, _bindingList, true))))
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
