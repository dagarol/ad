using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;


namespace Serpis.Ad
{
	public class ModelHelper
	{
		public static string GetSelect(Type type){
			string keyName;
			List<string> fieldNames = new List<string>();
			foreach (PropertyInfo propertyInfo in type.GetProperties ()){
				if (propertyInfo.IsDefined (typeof(keyAttribute), true))
					keyName = propertyInfo.Name;
				else if(propertyInfo.IsDefined (typeof(FieldAttribute), true))
					fieldNames.Add (propertyInfo.Name);
				
				
			}
			string tableName = type.Name.ToLower();
			return string.Format ("select{0} from {1} where {2} = ",
			                      string.Join(", ", fieldNames),
			                      tableName, keyName);
		}
		public static object Load(Type type, string id){
			IDbCommand selectDbCommand = App.Instance.DbConnection.CreateCommand();
			selectDbCommand.CommandText = GetSelect(type) + id;
			IDataReader dataReader = selectDbCommand.ExecuteReader();
			dataReader.Read();
			
			object obj = Activator.CreateInstance(type);
			foreach (PropertyInfo propertyInfo in type.GetProperties ())
            {
                if (propertyInfo.IsDefined (typeof(KeyAttribute), true))
                    propertyInfo.SetValue(obj, id, null);
				
                else if (propertyInfo.IsDefined (typeof(FieldAttribute), true))
                    propertyInfo.SetValue(obj, dataReader[propertyInfo.Name.ToLower()], null); //TODO convert al tipo de destino
            }
			dataReader.Close ();
            return obj;
					
		}
		
		public static void Save(object obj){
           
            IDbCommand updateDbCommand = App.Instance.DbConnection.CreateCommand();
            Type type = obj.GetType();
            updateDbCommand.CommandText = GetUpdate(type);
           
            foreach (PropertyInfo propertyInfo in type.GetProperties()){
                if(propertyInfo.IsDefined (typeof(KeyAttribute), true)
                    || propertyInfo.IsDefined (typeof(FieldAttribute), true)){
                   
                    object value = propertyInfo.GetValue(obj, null);
                    DbCommandUtil.AddParameter(updateDbCommand, propertyInfo.Name.ToLower(), value);
                }
                updateDbCommand.ExecuteNonQuery();
               
            }
	}
}
	
	
