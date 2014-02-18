using System;
using NUnit.Framework;

namespace Serpis.Ad
{
	internal class ModelHelperFoo {
		[Key]
		public int Id {get;set;}
		[Field]
		public string Nombre {get;set;}
	}
	
	internal class ModelHelperBar {
		[Key]
		public int Id {get;set;}
		[Field]
		public string Nombre {get;set;}
		[Field]
		public decimal precio {get;set;}
	}
		
	[TestFixture()]
	public class ModelHelperTest
	{
		[Test()]
		public void GetSelect ()
		{
			string selectText;
			string expected ;
			
			selectText = ModelHelper.GetSelect (typeof(ModelHelperFoo));
			expected = "select nombre from modelhelperfoo where id=";
			Assert.AreEqual (expected, selectText);
			
			selectText = ModelHelper.GetSelect (typeof(ModelHelperBar));
			expected = "select nombre, precio from modelhelperbar where id=";
			Assert.AreEqual (expected, selectText);
		}
		
		[Test()]
		public void GetUpdate ()
		{
			string updateText;
			string expected ;
			
			updateText = ModelHelper.GetUpdate (typeof(ModelHelperFoo));
			expected = "update modelhelperfoo set nombre=@nombre where id=@id";
			Assert.AreEqual (expected, updateText);
			
			updateText = ModelHelper.GetUpdate (typeof(ModelHelperBar));
			expected = "update modelhelperbar set nombre=@nombre, precio=@precio where id=@id";
			Assert.AreEqual (expected, updateText);
		}
	}
}

