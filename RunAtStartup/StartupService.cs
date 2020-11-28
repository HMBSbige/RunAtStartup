using Microsoft.Win32;
using RunAtStartup.Enums;
using System;
using System.Linq;

namespace RunAtStartup
{
	public class StartupService
	{
		public StartupType Type { get; }
		public string Key { get; }

		private const string RegistryRunPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

		public StartupService(string key, StartupType type = StartupType.CurrentUser)
		{
			Key = key;
			Type = type;
		}

		private RegistryKey? OpenRegKey(string name, bool writable)
		{
			var hive = Type == StartupType.LocalMachine ? RegistryHive.LocalMachine : RegistryHive.CurrentUser;
			var view = Environment.Is64BitProcess ? RegistryView.Registry64 : RegistryView.Registry32;
			return RegistryKey.OpenBaseKey(hive, view).OpenSubKey(name, writable);
		}

		public bool Check()
		{
			using var runKey = OpenRegKey(RegistryRunPath, false);
			if (runKey is null)
			{
				throw new SystemException(@"Cannot open Registry!");
			}

			var runList = runKey.GetValueNames();

			return runList.Any(item => item == Key);
		}

		public void Set(string data)
		{
			using var runKey = OpenRegKey(RegistryRunPath, true);
			if (runKey is null)
			{
				throw new SystemException(@"Cannot open Registry!");
			}
			runKey.SetValue(Key, data);
		}

		public void Delete()
		{
			using var runKey = OpenRegKey(RegistryRunPath, true);
			if (runKey is null)
			{
				throw new SystemException(@"Cannot open Registry!");
			}

			runKey.DeleteValue(Key);
		}
	}
}
