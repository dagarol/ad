using System;

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
			IDataReader
		}
		public static object Save (Categoria categoria){
			//save
		}
	}
}

