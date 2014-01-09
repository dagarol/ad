using Gtk;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Serpis.Ad
{
	public class CategoriaListView : EntityListView
	{
		public CategoriaListView ()
		{
			App.Instance.DbConnection = new MySqlConnection(
				"Server=localhost;Database=dbprueba;User Id=root;Password=sistemas"
			);
			TreeViewHelper treeViewHelper = new TreeViewHelper(
				treeView, 
				App.Instance.DbConnection, 
				"select id, nombre from categoria order by nombre desc"
			);
			
			Gtk.Action addAction = new Gtk.Action("addAction", null, null, Stock.Add);
			addAction.Activated += delegate {
				IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
				dbCommand.CommandText = 
					string.Format ("insert into categoria (nombre) values ('{0}')", DateTime.Now);
				dbCommand.ExecuteNonQuery ();
			};
			actionGroup.Add (addAction);
			
			Gtk.Action removeAction = new Gtk.Action("removeAction", null, null, Stock.Remove);
			removeAction.Activated += delegate {
				IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
				dbCommand.CommandText = 
					string.Format ("delete from categoria where id={0}", treeViewHelper.Id);
				dbCommand.ExecuteNonQuery ();
			};
			actionGroup.Add(removeAction);

			Gtk.Action refreshAction = new Gtk.Action("refreshAction", null, null, Stock.Refresh);
			refreshAction.Activated += delegate {treeViewHelper.Refresh ();	};
			actionGroup.Add (refreshAction);
			
			treeView.Selection.Changed += delegate {
				Console.WriteLine("treeViewHelper.Id='{0}'", treeViewHelper.Id);
				removeAction.Sensitive = treeView.Selection.CountSelectedRows() > 0;
			};
			
			removeAction.Sensitive = false;
		}

	}
}

