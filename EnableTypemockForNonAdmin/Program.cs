using System;
using System.Reflection;

namespace EnableTypemockForNonAdmin
{
	public class Program
	{
		public static string ProgramName
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Name;
			}
		}

		public static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				WriteHelp();
				return;
			}

			Console.WriteLine("{0} - Setting up Typemock/Profiler permissions.", ProgramName);
			Console.WriteLine("----------");

			string usernameArg = args[0];
			if (!ValidateUsername(usernameArg))
			{
				return;
			}
			Console.WriteLine("Validated account {0}.", usernameArg);
			if (!SetFilePermissions(usernameArg))
			{
				return;
			}
			if (!SetRegistryPermissions(usernameArg))
			{
				return;
			}

			Console.WriteLine("Complete.");
			DebuggerPause();
		}

		public static void DebuggerPause()
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				Console.WriteLine("Press any key to exit...");
				Console.ReadKey();
			}
		}

		public static void HandleError(string message)
		{
			Console.WriteLine("*** {0}", message);
			WriteHelp();
			DebuggerPause();
		}

		private static bool SetFilePermissions(string username)
		{
			var fileCatalog = new FileCatalog();
			var typemockInstallDir = fileCatalog.TypemockInstallDirectory;
			if (typemockInstallDir == null || !typemockInstallDir.Exists)
			{
				HandleError("Unable to find Typemock install folder.");
				return false;
			}

			foreach (var file in fileCatalog.Files)
			{
				if (!file.Exists)
				{
					continue;
				}
				try
				{
					file.AllowFullControl(username);
				}
				catch (Exception ex)
				{
					HandleError(ex.Message);
					return false;
				}
				Console.WriteLine("Updated permissions for {0}.", file.FullName);
			}
			return true;
		}

		private static bool SetRegistryPermissions(string username)
		{
			var catalog = new RegistryKeyCatalog();
			foreach (var key in catalog.ProfilerKeys)
			{
				try
				{
					key.AllowFullControl(username);
				}
				catch (Exception ex)
				{
					HandleError(ex.Message);
					return false;
				}
				Console.WriteLine("Updated permissions for {0} [{1}].", key.Name, key.View);
			}
			return true;
		}

		private static bool ValidateUsername(string username)
		{
			try
			{
				Username u = new Username(username);
				if (!u.IsValidNtAccount)
				{
					HandleError(String.Format("Unable to validate username {0}.", u.BaseName));
					return false;
				}
			}
			catch (Exception ex)
			{
				HandleError(ex.Message);
				return false;
			}
			return true;
		}

		public static void WriteHelp()
		{
			string name = ProgramName;
			Console.WriteLine("{0} - Set up Typemock and profilers for a non-admin user.", name);
			Console.WriteLine("Usage:");
			Console.WriteLine("{0} username", name);
			Console.WriteLine("Example:");
			Console.WriteLine(@"{0} YOURDOMAIN\yourusername", name);
			DebuggerPause();
		}
	}
}
