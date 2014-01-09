using System;
using Gtk;
using System.Data;
namespace Serpis.Ad
{
	public class CategoriaListView : EntityListView
	{
		public CategoriaListView ()
		{
			TreeViewHelper t=new TreeViewHelper(treeView,"Select * from categoria");
			Gtk.Action refreshAction = new Gtk.Action("refreshAction", null, null, Stock.Refresh);
			//tengo acceso al actionGroup de IEntityListView
			actionGroup.Add(refreshAction);
			refreshAction.Activated += delegate {t.Refresh ();};
			Gtk.Action editAction = new Gtk.Action("editAction", null, null, Stock.Edit);
			actionGroup.Add(editAction);
			editAction.Activated += delegate {
				Window ventana=new Window("Editar");
				ventana.SetDefaultSize(500,500);
				VBox h=new VBox(true,10);
				ventana.Add (h);
				Label enunciado=new Label("Introduce el nuevo valor:");
				h.Add (enunciado);
				Entry caja=new Entry();
				h.Add (caja);
				Button b=new Button("Editar");
				h.Add (b);
				b.Clicked+=delegate
				{
					IDbCommand dbCommand=App.Instance.DbConnection.CreateCommand();		
					dbCommand.CommandText = 
					string.Format ("update categoria set nombre='{1}' where id={0}", t.Id,caja.Text);
					dbCommand.ExecuteNonQuery ();
				};
				
				ventana.ShowAll();
				
			};
			
			
		}
	}
}

