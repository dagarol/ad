using Gtk;
using System;
using System.Data;
namespace Serpis.Ad
{
	public partial class BotonAdd : Gtk.Window
	{
		public BotonAdd () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			aceptar.Clicked+=delegate
				{
					IDbCommand dbCommand=App.Instance.DbConnection.CreateCommand();	
					dbCommand.CommandText = 
					string.Format ("insert into articulo (nombre) values ('{0}')", caja.Text);
					Console.WriteLine (caja.Text);
					dbCommand.ExecuteNonQuery ();
				};
		}
	}
}

