using Gtk;
using System;
using System.Data;

namespace Serpis.Ad
{
	public partial class BotonDelete : Gtk.Window
	{
		public BotonDelete () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			eliminar.Clicked+=delegate
			{
				IDbCommand dbCommand=App.Instance.DbConnection.CreateCommand();		
				dbCommand.CommandText = 
				string.Format ("delete from articulo where id={0}", cajaBorrar.Text);
				dbCommand.ExecuteNonQuery ();
			};
		}
	}
}

