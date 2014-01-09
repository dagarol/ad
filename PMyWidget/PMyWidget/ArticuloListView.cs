using Gtk;
using System;

namespace Serpis.Ad
{
	public class ArticuloListView : EntityListView
	{
		public ArticuloListView ()
		{
			treeView.AppendColumn("id", new CellRendererText(), "text", 0);
			treeView.AppendColumn("nombre", new CellRendererText(), "text", 1);
			treeView.AppendColumn("categoria", new CellRendererText(), "text", 2);
			treeView.AppendColumn("precio", new CellRendererText(), "text", 3);
			
			ListStore listStore = new ListStore(typeof(int), typeof(string), typeof(string), typeof(string));
			listStore.AppendValues(1, "Artículo 1", "Categoría 1", "1.5");
			listStore.AppendValues(2, "Artículo 2", "Categoría 2", "2.5");
			
			treeView.Model = listStore;
			
			Gtk.Action editAction = new Gtk.Action("editAction", null, null, Stock.Edit);
			actionGroup.Add (editAction);

			Gtk.Action newAction = new Gtk.Action("newAction", null, null, Stock.New);
			actionGroup.Add (newAction);
			
			editAction.Sensitive = false;
			treeView.Selection.Changed += delegate {
				editAction.Sensitive = treeView.Selection.CountSelectedRows() > 0;
			};
		}
		
		
	}
}

