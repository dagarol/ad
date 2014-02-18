using System;
using System.Reflection;
using System.Collections.Generic;
 
namespace Serpis.Ad
{
    public class ModelInfo
    {
        public string TableName { get {return tableName;} }
        private Type type;
        internal ModelInfo (Type type)
             
        {
            this.type = type;
            tableName = type.Name.ToLower ();
            fieldPropertyInfos=new List<PropertyInfo>();
            fieldNames=new List<string>();
            allNames=new List<string>();
            fieldParameters=new List<string>();
            foreach (PropertyInfo propertyInfo in type.GetProperties()) 
            {
                if (propertyInfo.IsDefined (typeof(KeyAttribute), true)) 
                {
                    keyPropertyInfo = propertyInfo;
                    keyName = propertyInfo.Name.ToLower ();
                    keyParameter=formatParameter(propertyInfo.Name.ToLower ());
                    allNames.Add (formatParameter(propertyInfo.Name.ToLower()));
                } 
                else if (propertyInfo.IsDefined (typeof(FieldAttribute), true)) 
                {
                    fieldPropertyInfos.Add (propertyInfo);
                    fieldParameters.Add(formatParameter(propertyInfo.Name.ToLower()));
                    fieldNames.Add (propertyInfo.Name.ToLower());
                    allNames.Add (formatParameter(propertyInfo.Name.ToLower()));
                }

				
				
            getSelect=string.Format ("select {0} from {1} where {2}=",string.Join(", ", fieldNames), tableName, keyName);
            getUpdate=string.Format ("update {1} set {0} where {2}",string.Join(", ", fieldParameters), tableName, keyParameter);
            getInsert=string.Format ("insert into {1} set {0}",string.Join(", ", allNames), tableName);         
            }
             
        }
 
        private string tableName;
        private List<string> fieldNames;
        private List<string> fieldParameters;
        private string keyParameter;
        private string keyName;
        private PropertyInfo keyPropertyInfo;
        public PropertyInfo KeyPropertyInfo { get { return keyPropertyInfo; } }
        public string KeyName { get {return keyName;}}
        private List<PropertyInfo> fieldPropertyInfos;
        public PropertyInfo[] FieldPropertyInfos {get {return fieldPropertyInfos.ToArray();}}
        public string[] FieldNames {get {return fieldNames.ToArray();}}
        public string getSelect;
        public string getUpdate;
        public string getInsert;
        public List <string> allNames;
 
        private static string formatParameter(string field)
        {
            return string.Format ("{0}=@{0}", field);     
        }
    }
}


