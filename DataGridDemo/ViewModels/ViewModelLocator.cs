using Assisticant;
using DataGridDemo.Models;

namespace DataGridDemo.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private Document _document;
		private Selection _selection;

        public ViewModelLocator()
        {
			if (DesignMode)
				_document = LoadDesignModeDocument();
			else
				_document = LoadDocument();
			_selection = new Selection();
        }

        public object Main =>
            ViewModel(() => new MainViewModel(_document, _selection));

        public object Item =>
            ViewModel(() => _selection.SelectedItem == null
			    ? null
			    : new ItemViewModel(_selection.SelectedItem));

        private Document LoadDocument()
		{
			// TODO: Load your document here.
            Document document = new Document();
            var one = document.NewItem();
            one.Name = "One";
            var two = document.NewItem();
            two.Name = "Two";
            var three = document.NewItem();
            three.Name = "Three";
            return document;
		}

		private Document LoadDesignModeDocument()
		{
            Document document = new Document();
            var one = document.NewItem();
            one.Name = "Design";
            var two = document.NewItem();
            two.Name = "Mode";
            var three = document.NewItem();
            three.Name = "Data";
            return document;
		}
    }
}
