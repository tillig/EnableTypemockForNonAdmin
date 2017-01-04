using System;
using EnableTypemockForNonAdmin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;

namespace Test
{
	[TestClass]
	public class RegistryKeyExtensionsFixture
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AllowFullControl_NullRegistryKey()
		{
			RegistryKey key = null;
			key.AllowFullControl("username");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void AllowFullControl_EmptyUsername()
		{
			RegistryKey key = Registry.CurrentUser;
			key.AllowFullControl("");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AllowFullControl_NullUsername()
		{
			RegistryKey key = Registry.CurrentUser;
			key.AllowFullControl(null);
		}
	}
}
