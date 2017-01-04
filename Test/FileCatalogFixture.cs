using System;
using System.Linq;
using EnableTypemockForNonAdmin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
	[TestClass]
	public class FileCatalogFixture
	{
		[TestMethod]
		public void TypemockInstallDirectory_Located()
		{
			var catalog = new FileCatalog();
			var installDir = catalog.TypemockInstallDirectory;
			Assert.IsTrue(installDir.FullName.Contains(@"\Typemock\Isolator\"));
		}

		[TestMethod]
		public void Files_ContainsTypemockConfig()
		{
			var catalog = new FileCatalog();
			var count = catalog.Files.Where(file => String.CompareOrdinal(file.Name, "typemockconfig.xml") == 0).Count();
			Assert.AreEqual(1, count);
		}
	}
}
