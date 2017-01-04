using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace EnableTypemockForNonAdmin
{
	public class FileCatalog
	{
		public IEnumerable<FileInfo> Files
		{
			get
			{
				yield return new FileInfo(Path.Combine(this.TypemockInstallDirectory.FullName, "typemockconfig.xml"));
			}
		}

		public DirectoryInfo TypemockInstallDirectory
		{
			get
			{
				var regValue = Registry.GetValue(@"HKEY_CLASSES_ROOT\CLSID\{B146457E-9AED-4624-B1E5-968D274416EC}\InprocServer32", "", null);
				if (regValue == null)
				{
					return null;
				}
				var typemockInstallDir = Directory.GetParent(Path.GetDirectoryName(regValue.ToString()));
				return typemockInstallDir;
			}
		}
	}
}
