using System;
using EnableTypemockForNonAdmin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
	[TestClass]
	public class UsernameFixture
	{
		private const string ValidUsername = @"HIL-TILLIG\LocalAdmin";

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_BaseNameEmpty()
		{
			Username u = new Username("");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_BaseNameNull()
		{
			Username u = new Username(null);
		}

		[TestMethod]
		public void Ctor_BaseNameSet()
		{
			Username u = new Username(ValidUsername);
			Assert.AreEqual(ValidUsername, u.BaseName);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_BaseNameWhitespace()
		{
			Username u = new Username("  ");
		}

		[TestMethod]
		public void IsValidNtAccount_NotValidAccount()
		{
			Username u = new Username(@"MACHINENAME\NoSuchAccount");
			Assert.IsFalse(u.IsValidNtAccount);
		}

		[TestMethod]
		public void IsValidNtAccount_ValidAccount()
		{
			Username u = new Username(ValidUsername);
			Assert.IsTrue(u.IsValidNtAccount);
		}
	}
}
