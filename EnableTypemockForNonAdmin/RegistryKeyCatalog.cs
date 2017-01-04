using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Win32;

namespace EnableTypemockForNonAdmin
{
	public class RegistryKeyCatalog
	{
		private static readonly string[] HkcrClsidRegistryKeys = new string[]
		{
			// HKEY_CLASSES_ROOT\{0}CLSID\ where {0} gets substituted with "" for 64-bit and "Wow6432Node\" for 32-bit.
			@"{B146457E-9AED-4624-B1E5-968D274416EC}", // Typemock
			@"{BC0AFDDB-7093-4752-A891-F15BA4FCFB0D}", // TestRunner
			@"{9721F7EB-5F92-447C-9F75-79278052B7BA}", // NCover 3.x
			@"{4BD66EB5-1F60-4bbd-8820-5E13080D49BE}", // NCover 1.3
			@"{6287B5F9-08A1-45E7-9498-B5B2E7B02995}", // NCover
			@"{18656C37-035D-41CD-82C2-85DEF2DD5F7B}", // CoverageEye
			@"{372DC2B6-0A34-4f22-BC34-BE7A18DE9137}", // DevPartner
		};

		public IEnumerable<RegistryKey> ProfilerKeys
		{
			get
			{
				foreach (var leaf in HkcrClsidRegistryKeys)
				{
					var rk = this.GetHkcrClsidKey(leaf, RegistryView.Registry32);
					if (rk != null)
					{
						yield return rk;
					}
					rk = this.GetHkcrClsidKey(leaf, RegistryView.Registry64);
					if (rk != null)
					{
						yield return rk;
					}
				}
			}
		}

		private RegistryKey GetHkcrClsidKey(string leaf, RegistryView bitness)
		{
			var rk = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, bitness);
			if (rk == null)
			{
				return null;
			}
			rk = rk.OpenSubKey("CLSID");
			if (rk == null)
			{
				return null;
			}
			rk = rk.OpenSubKey(leaf, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.ChangePermissions);
			if (rk == null)
			{
				return null;
			}
			return rk;
		}
	}
}
