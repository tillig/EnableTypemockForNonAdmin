using System;
using System.IO;
using System.Security.AccessControl;

namespace EnableTypemockForNonAdmin
{
	public static class FileInfoExtensions
	{
		public static void AllowFullControl(this FileInfo file, string username)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			if (username == null)
			{
				throw new ArgumentNullException("username");
			}
			if (username.Length == 0)
			{
				throw new ArgumentException("Username may not be empty.", "username");
			}
			if (!file.Exists)
			{
				throw new FileNotFoundException(String.Format("Unable to find file {0}", file.FullName), file.FullName);
			}
			var acl = file.GetAccessControl();
			var rule = new FileSystemAccessRule(username, FileSystemRights.FullControl, AccessControlType.Allow);
			acl.AddAccessRule(rule);
			file.SetAccessControl(acl);
		}
	}
}
