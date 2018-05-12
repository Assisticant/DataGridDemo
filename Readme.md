# GridView Demo

Demonstrates how Assisticant can be used with a control that expects a BindingList<T>. Use the [following conventions](http://assisticant.net/collections.html#bindinglistt) within the view model:

- A property that returns `IEnumerable<ItemType>`
- A method called `NewItemIn` and the name of the property that returns `ItemType`.
- A method called `DeleteItemFrom` and the name of the property that takes `ItemType`.

Previous versions of this demo included a class called `BindingListAdapter`. Look at the revision history to see how much code can be saved by using the new conventions.

## AutoGenerateColumns

Set AutoGenerateColumns to False. Instead, explicitly set the DataGrid.Columns collection and use the Binding property of each column to select the property from the child view model. If you auto generate columns, then all of the child view model properties will appear, including the one that accesses the raw model.

## IEditableObject

The ItemRow class demonstrates how to use IEditableObject in a child view model. Keep an observable Boolean indicating whether the user is editing the object. Use this as a switch on the properties for whether to return the cached observable value or the actual model value. Capture the model value when the user starts editing, and store the cached value into the model when they are done.

A more sophisticated implementation would capture version information when the user begins editing. Then when they are finished, it would implement either an optimistic concurrency check or a parallel historical fact. Optimistic concurrency would reject the user's input in the face of a conflict, while a parallel fact would capture it and present it to be resolved later. Either way, the concurrency is based on the moment that editing began, not the moment it ended.

Help for editable objects may or may not be added to the Assisticant library in the future. For now, it simply passes the IEditableObject implementation through the wrapper so that you can implement your own mechanism. 