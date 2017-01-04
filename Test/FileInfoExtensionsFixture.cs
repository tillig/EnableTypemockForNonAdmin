using System;
using System.IO;
using System.Reflection;
using EnableTypemockForNonAdmin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
	[TestClass]
	public class FileInfoExtensionsFixture
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AllowFullControl_NullFileInfo()
		{
			FileInfo file = null;
			file.AllowFullControl("username");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void AllowFullControl_EmptyUsername()
		{
			FileInfo file = new FileInfo(Assembly.GetExecutingAssembly().CodeBase);
			file.AllowFullControl("");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void AllowFullControl_NullUsername()
		{
			FileInfo file = new FileInfo(Assembly.GetExecutingAssembly().CodeBase);
			file.AllowFullControl(null);
		}

		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void AllowFullControl_FileDoesNotExist()
		{
			FileInfo file = new FileInfo(@"C:\No\Such\File.txt");
			file.AllowFullControl("username");
		}
	}
}
