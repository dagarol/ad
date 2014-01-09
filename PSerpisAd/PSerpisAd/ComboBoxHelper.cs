using Gtk;
using System;
using System.Data;

namespace Serpis.Ad
{
	public class ComboBoxHelper
	{
		private const string selectFormat = "select {0}, {1} from {2}";
		
		private ComboBox comboBox;
		private ListStore listStore;
		
		public ComboBoxHelper (
			ComboBox comboBox, 
			IDbConnection dbConnection, 
			string keyFieldName, 
			string valueFieldName, 
			string tableName, 
			int id) {
			
			this.comboBox = comboBox;
			
			CellRendererText cellRendererText = new CellRendererText();
			comboBox.PackStart (cellRendererText, true);
			comboBox.AddAttribute (cellRendererText, "text", 1);
			
			listStore = new ListStore(typeof(int), typeof(string));
			//TODO localización para "sin asignar"
			TreeIter initialTreeIter = listStore.AppendValues(0, "<sin asignar>");
			IDbCommand dbCommand = dbConnection.CreateCommand ();
			dbCommand.CommandText = string.Format(selectFormat, keyFieldName, valueFieldName, tableName);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			while (dataReader.Read ()) {
				int key = (int)dataReader[keyFieldName];
				string value = (string)dataReader[valueFieldName];
				TreeIter treeIter = listStore.AppendValues (key, value);
				if (key == id)
					initialTreeIter = treeIter;
			}
			dataReader.Close ();
			
			comboBox.Model = listStore;
			comboBox.SetActiveIter (initialTreeIter);
			
		}
		
		public int Id {
			get {
				TreeIter treeIter;
				comboBox.GetActiveIter(out treeIter);
				int id = (int)listStore.GetValue (treeIter, 0);
				return id;
			}
		}
	}
}

