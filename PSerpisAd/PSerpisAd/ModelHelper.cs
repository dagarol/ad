using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;


namespace Serpis.Ad
{
	public class ModelHelper
	{
		public static string GetSelect(Type type)
		{
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
		
		public static object Load(Type type, string id)
		{
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
                    propertyInfo.SetValue(obj, dataReader[propertyInfo.Name.ToLower()], null); 
            }
			dataReader.Close ();
            return obj;
					
		}
	
		
		public static void Save(object obj)
		{
           
            IDbCommand updateDbCommand = App.Instance.DbConnection.CreateCommand();
            Type type = obj.GetType();
            updateDbCommand.CommandText = GetUpdate(type);
           
            foreach (PropertyInfo propertyInfo in type.GetProperties())
			{
                if(propertyInfo.IsDefined (typeof(KeyAttribute), true)
                    || propertyInfo.IsDefined (typeof(FieldAttribute), true))
				{
                   
                    object value = propertyInfo.GetValue(obj, null);
                    DbCommandUtil.AddParameter(updateDbCommand, propertyInfo.Name.ToLower(), value);
                }
                updateDbCommand.ExecuteNonQuery();
               
            }
		}
			
		 public static void Insert(object obj)
        {
            IDbCommand insertDbCommand = App.Instance.DbConnection.CreateCommand();
            Type type = obj.GetType();
            insertDbCommand.CommandText = GetInsert(type);

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if(propertyInfo.IsDefined (typeof(KeyAttribute), true) || propertyInfo.IsDefined (typeof(FieldAttribute), true))
                {
                    object value = propertyInfo.GetValue(obj, null);
                    DbCommandUtil.AddParameter(insertDbCommand, propertyInfo.Name.ToLower(), value);
                }
                insertDbCommand.ExecuteNonQuery();
            }
        }
		
		
		private static string formatParameter(string field)
        {
            return string.Format ("{0}=@{0}", field);   
        }
       
        public static string GetUpdate(Type type)
        {
            string keyParameter = null;
            List<string> fieldParameters = new List<string>();
           
            foreach (PropertyInfo propertyInfo in type.GetProperties ())
            {
                if (propertyInfo.IsDefined (typeof(KeyAttribute), true))
                    keyParameter = formatParameter(propertyInfo.Name.ToLower ());
               
                else if (propertyInfo.IsDefined (typeof(FieldAttribute), true))
                    fieldParameters.Add (formatParameter(propertyInfo.Name.ToLower()));
            }
           
            string tableName = type.Name.ToLower();
           
            return string.Format ("update {1} set {0} where {2}",
                                  string.Join(", ", fieldParameters), tableName, keyParameter);
        }
       
        public static string GetInsert(Type type)
        {
            List<string> allParameters = new List<string>();
           
            foreach (PropertyInfo propertyInfo in type.GetProperties ())
            {
                if (propertyInfo.IsDefined (typeof(KeyAttribute), true) || propertyInfo.IsDefined (typeof(FieldAttribute), true))
                    allParameters.Add (formatParameter(propertyInfo.Name.ToLower()));
            }
           
            string tableName = type.Name.ToLower();
           
            return string.Format ("insert into {1} set {0}",string.Join(", ", allParameters), tableName);
        }
			
	}
}
	
	
	

