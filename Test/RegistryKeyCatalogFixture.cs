using System;
using System.Linq;
using EnableTypemockForNonAdmin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
	[TestClass]
	public class RegistryKeyCatalogFixture
	{
		[TestMethod]
		public void ProfilerKeys_ContainsNCoverKeys()
		{
			var catalog = new RegistryKeyCatalog();
			var count = catalog.ProfilerKeys.Where(key => key.Name.Contains(@"{9721F7EB-5F92-447C-9F75-79278052B7BA}")).Count();
			Assert.AreEqual(2, count); // 32-bit and 64-bit keys
		}

		[TestMethod]
		public void ProfilerKeys_ContainsTypemockKeys()
		{
			var catalog = new RegistryKeyCatalog();
			var count = catalog.ProfilerKeys.Where(key => key.Name.Contains(@"{B146457E-9AED-4624-B1E5-968D274416EC}")).Count();
			Assert.AreEqual(2, count); // 32-bit and 64-bit keys
		}

		[TestMethod]
		public void ProfilerKeys_DoesNotContainsArbitraryKeys()
		{
			var catalog = new RegistryKeyCatalog();
			var count = catalog.ProfilerKeys.Where(key => key.Name.Contains(@"{4B9D3A8C-033E-4F6E-9464-1E1E8604C0C3}")).Count();
			Assert.AreEqual(0, count);
		}
	}
}
