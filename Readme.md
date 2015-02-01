# GridView Demo

Demonstrates how Assisticant can be used with a control that expects a BindingList<T>. At the current state, this demo is far too involved. The reusable code in the Containers folder should become part of the library itself, at which point this demo will become a simple illustration of how to use that class.

## BindingListAdapter

Nevertheless, here's how the code currently works. The BindingListAdapter class in the Containers folder is the primary logic. It uses a ComuptedSubscription and object recycling to synchronize a BindingList<object> with a source collection. And when the control manipulates the BindingList<object>, it calls delegates to make the corresponding changes in the source collection.

To construct a BindingListAdapter, provide four delegates.

* **GetItems**: Produce the collection of items to place in the list. This could be raw model objects, but I recommend view models. If you do provide a list of view models, use Select(m => new VM(m)) to project into a collection of view models.
* **Factory**: Create a new instance of the item in the list. *Do not add the object to the source list.* If the list contains view models, as I recommend, create a new model and a new view model.
* **Insert**: Add the provided item to the source list at the specified index. If the item is a view model, extract the model from it and add it to the list of models.
* **Remove**: Remove the item at the specified index.

Return the BindingList property from your view model to data bind to the GridView.

## AutoGenerateColumns

Set AutoGenerateColumns to False. Instead, explicitly set the DataGrid.Columns collection and use the Binding property of each column to select the property from the child view model. If you auto generate columns, then all of the child view model properties will appear, including the one that accesses the raw model.