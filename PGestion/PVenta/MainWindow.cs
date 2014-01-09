using System;
using Gtk;
using Serpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		
		Build ();
		
		ArticuloListView a= new ArticuloListView();
		CategoriaListView c=new CategoriaListView();
		
		notebook2.AppendPage(a,new Label("articulo"));
		notebook2.AppendPage(c,new Label("categoria"));
		
		UiManagerHelper acciones=new UiManagerHelper(UIManager);
		
		acciones.SetActionGroup(a.ActionGroup);
		
		notebook2.SwitchPage+=delegate{
			
			IEntityListView i=(IEntityListView)notebook2.CurrentPageWidget;
			acciones.SetActionGroup(i.ActionGroup);
			
		};
	}
//		mySqlConnection = new MySqlConnection("Server=localhost;Database=dbPrueba;User Id=root;Password=sistemas");
//		mySqlConnection.Open ();
//		
//		string selectSql = 
//			"select id, nombre from articulo";
//		TreeViewHelper treeViewHelper = new TreeViewHelper(treeView, mySqlConnection, selectSql);
//		
//		ListStore listStore = treeViewHelper.ListStore; 
//		editAction.Sensitive = false;
//		deleteAction.Sensitive = false;
//		
//		editAction.Activated += delegate {
//			if (treeView.Selection.CountSelectedRows() == 0)
//				return;
//			TreeIter treeIter;
//			treeView.Selection.GetSelected(out treeIter);
//			object id = listStore.GetValue (treeIter, 0);
//			object nombre = listStore.GetValue (treeIter, 1);
//			
//			MessageDialog messageDialog = new MessageDialog(this,
//                DialogFlags.DestroyWithParent,
//                MessageType.Info,
//                ButtonsType.Ok,
//                "Seleccionado Id={0} Nombre={1}", id, nombre);
//			messageDialog.Title = "Este es el título del mensaje";
//			messageDialog.Run ();
//			messageDialog.Destroy ();
//		};
	
	
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}