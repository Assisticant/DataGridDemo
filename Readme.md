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

## IEditableObject

The ItemRow class demonstrates how to use IEditableObject in a child view model. Keep an observable Boolean indicating whether the user is editing the object. Use this as a switch on the properties for whether to return the cached observable value or the actual model value. Capture the model value when the user starts editing, and store the cached value into the model when they are done.

A more sophisticated implementation would capture version information when the user begins editing. Then when they are finished, it would implement either an optimistic concurrency check or a parallel historical fact. Optimistic concurrency would reject the user's input in the face of a conflict, while a parallel fact would capture it and present it to be resolved later. Either way, the concurrency is based on the moment that editing began, not the moment it ended.

Help for editable objects may or may not be added to the Assisticant library in the future. For now, it simply passes the IEditableObject implementation through the wrapper so that you can implement your own mechanism. 