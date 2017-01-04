using System;
using System.Security.Principal;

namespace EnableTypemockForNonAdmin
{
	public class Username
	{
		public string BaseName { get; private set; }

		public bool IsValidNtAccount
		{
			get
			{
				NTAccount account = new NTAccount(this.BaseName);
				try
				{
					account.Translate(typeof(SecurityIdentifier));
				}
				catch (IdentityNotMappedException)
				{
					return false;
				}
				return true;
			}
		}

		public Username(string baseName)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (String.IsNullOrWhiteSpace(baseName))
			{
				throw new ArgumentException("Base name may not be null, empty, or whitespace.", "baseName");
			}
			this.BaseName = baseName;
		}
	}
}
