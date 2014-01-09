using Gtk;
using System;

namespace Serpis.Ad
{
	[System.ComponentModel.ToolboxItem(true)]
	public abstract partial class MyWidget : Gtk.Bin, IEntityListView
	{
		public MyWidget ()
		{
			this.Build ();
			Visible = true;
			
			treeView.AppendColumn("id", new CellRendererText(), "text", 0);
			treeView.AppendColumn("nombre", new CellRendererText(), "text", 1);
			
			ListStore listStore = new ListStore(typeof(int), typeof(string));
			listStore.AppendValues(1, "Elemento 1");
			listStore.AppendValues(2, "Elemento 2");
			
			treeView.Model = listStore;
			
			treeView.Selection.Changed += delegate {
			};
		}
		
		public TreeView TreeView {
			get {return TreeView;}
		}
		

		public ActionGroup ActionGroup {
			get {return null;}
		}
	}
}

