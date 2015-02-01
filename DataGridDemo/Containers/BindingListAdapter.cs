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
        private BindingList<T> _bindingList;
        private ComputedSubscription _subscription;
        private List<ItemContainer<T>> _itemContainers = new List<ItemContainer<T>>();

        public BindingListAdapter(Func<IEnumerable<T>> getItems)
        {
            _bindingList = new BindingList<T>();
            _bindingList.AddingNew += AddingNewItem;
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
            throw new NotImplementedException();
        }

        private void UpdateBindingList(List<T> items)
        {
            using (var bin = new RecycleBin<ItemContainer<T>>(_itemContainers))
            {
                _itemContainers.Clear();
                foreach (var item in items)
                {
                    var itemContainer = bin.Extract(new ItemContainer<T>(item, _bindingList));
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
    }
}
