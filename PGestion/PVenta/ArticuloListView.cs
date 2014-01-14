using System;
using System.Data;
using Gtk;
namespace Serpis.Ad
{
	public class ArticuloListView : EntityListView
	{
		public ArticuloListView ()
		{
			TreeViewHelper t=new TreeViewHelper(treeView,"Select * from articulo");
			Gtk.Action addAction = new Gtk.Action("addAction", null, null, Stock.Add);
			
			actionGroup.Add(addAction);
			
			addAction.Activated += delegate 
			{
//				Window ventana=new Window("Añadir");
//				ventana.SetDefaultSize(500,500);
//				VBox h=new VBox();
//				ventana.Add (h);
//				
//				TextView textView = new TextView();
//				textView.Buffer.Text = "El texto del textView";
//				h.Add (textView);
//				Entry caja=new Entry();
//				h.Add (caja);
//				Button b=new Button("Añadir");
//				h.Add (b);
//				b.Clicked+=delegate
//				{
//					IDbCommand dbCommand=App.Instance.DbConnection.CreateCommand();	
//					dbCommand.CommandText = 
//					string.Format ("insert into articulo (nombre) values ('{0}')", caja.Text);
//					Console.WriteLine (caja.Text);
//					dbCommand.ExecuteNonQuery ();
//				};
//
//				ventana.ShowAll();
				new BotonAdd();
			};
			
			Gtk.Action refreshAction = new Gtk.Action("refreshAction", null, null, Stock.Refresh);
			
			actionGroup.Add(refreshAction);
			refreshAction.Activated += delegate {t.Refresh ();};
			Gtk.Action removeAction = new Gtk.Action("removeAction", null, null, Stock.Remove);
			actionGroup.Add(removeAction);
			removeAction.Activated += delegate 
			{
//				Window ventana=new Window("Borrar");
//				ventana.SetDefaultSize(500,500);
//				VBox h=new VBox(true,30);
//				ventana.Add (h);
//				Label enunciado=new Label("Introduce lo que quieras borrar:");
//				h.Add (enunciado);
//				Entry caja=new Entry();
//				h.Add (caja);
//				Button b=new Button("Borrar");
//				h.Add (b);
//				b.Clicked+=delegate
//				{
//					IDbCommand dbCommand=App.Instance.DbConnection.CreateCommand();		
//					dbCommand.CommandText = 
//					string.Format ("delete from articulo where id={0}", caja.Text);
//					dbCommand.ExecuteNonQuery ();
//				};
//				
//				ventana.ShowAll();
				new BotonDelete();
			};
			
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
					string.Format ("update articulo set nombre='{1}' where id={0}", t.Id,caja.Text);
					dbCommand.ExecuteNonQuery ();
				};
				
				ventana.ShowAll();
				
			};
			
			
		}
		
	}
}

