using Gtk;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using Serpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		IDbConnection dbConnection = new MySqlConnection(
			"Server=localhost;" +
			"Database=dbprueba;" +
			"User Id=root;" +
			"Password=sistemas");
		dbConnection.Open ();
		
		ComboBoxHelper comboBoxHelper = new ComboBoxHelper(
			comboBox,
			dbConnection,
			"id",
			"nombre",
			"categoria",
			2);
		
		comboBox.Changed += delegate {
			Console.WriteLine("comboBox.Changed id = {0}", comboBoxHelper.Id);
		};
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
