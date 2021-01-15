using Microsoft.Win32;
using RunAtStartup.Enums;
using System;

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

		public bool Check(string data)
		{
			using var runKey = OpenRegKey(RegistryRunPath, false);
			if (runKey is null)
			{
				throw new SystemException(@"Cannot open Registry!");
			}

			var value = runKey.GetValue(Key);

			if (value is not string str)
			{
				return false;
			}

			return str == data;
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

			try
			{
				runKey.DeleteValue(Key);
			}
			catch (ArgumentException)
			{
				// No value exists with that name.
			}
		}
	}
}
