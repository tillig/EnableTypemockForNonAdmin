using System;
using System.Security.AccessControl;
using Microsoft.Win32;

namespace EnableTypemockForNonAdmin
{
	public static class RegistryKeyExtensions
	{
		public static void AllowFullControl(this RegistryKey key, string username)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (username == null)
			{
				throw new ArgumentNullException("username");
			}
			if (username.Length == 0)
			{
				throw new ArgumentException("Username may not be empty.", "username");
			}
			var acl = key.GetAccessControl();
			var rule = new RegistryAccessRule(username, RegistryRights.FullControl, InheritanceFlags.None, PropagationFlags.None | PropagationFlags.InheritOnly, AccessControlType.Allow);
			acl.AddAccessRule(rule);
			rule = new RegistryAccessRule(username, RegistryRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None | PropagationFlags.InheritOnly, AccessControlType.Allow);
			acl.AddAccessRule(rule);
			key.SetAccessControl(acl);
		}
	}
}
