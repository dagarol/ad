using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PHolaMysql
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string connectionString = 
				"Server=localhost;" +
				"Database=dbprueba;" +
				"User Id=root;" +
				"password=sistemas";
			
			MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			
			mySqlConnection.Open();
			
			//select * from categoria
			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
			
			mySqlCommand.CommandText = "select * from categoria";
			
			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			
			int columns=mySqlDataReader.FieldCount;
			
			for(int i=0; i<columns; i++)
			{
			Console.Write(mySqlDataReader.GetName(i));
			Console.Write ("      ");
			}
			
		while(mySqlDataReader.Read())
			{
				for(int i=0; i<columns; i++)
				{
				Console.Write (i);
				}
			}
			
			Console.WriteLine(string.Join("    ",getColumnNames2(mySqlDataReader)));
			
			mySqlDataReader.Close();
			
			mySqlConnection.Close();
			
			Console.WriteLine ("OK");
		}
		private static string[] getColumnNames(MySqlDataReader mySqlDataReader) 
			{
			int fieldCount = mySqlDataReader.FieldCount;
			string[] columnNames = new string[ fieldCount ];
			for (int index = 0; index < fieldCount; index++)
				columnNames[index]=mySqlDataReader.GetName (index);
			return columnNames;
			}
		
		private static string[] getColumnNames2(MySqlDataReader mySqlDataReader) 
			{
			int fieldCount = mySqlDataReader.FieldCount;
			List<string> columnNames = new List<string>();
			for (int index=0; index <fieldCount; index++)
				columnNames.Add (mySqlDataReader.GetName(index) );
			return columnNames.ToArray();
			}
		
	}
}

