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
			expected = "select nombre from ModelHelperFoo where id=";
			Assert.AreEqual (expected, selectText);
			
			selectText = ModelHelper.GetSelect (typeof(ModelHelperBar));
			expected = "select nombre from ModelHelperBar where id=";
			Assert.AreEqual (expected, selectText);
		}
	}
}

