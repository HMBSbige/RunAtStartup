using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunAtStartup;
using RunAtStartup.Enums;

namespace UnitTest
{
	[TestClass]
	public class UnitTest
	{
		[TestMethod]
		public void CurrentUserTest()
		{
			const string key = @"RunAtStartup.CurrentUserTest";
			const string value = @"114514";
			var service = new StartupService(key);
			service.Delete();
			Assert.IsFalse(service.Check(value));

			service.Set(value);
			Assert.IsTrue(service.Check(value));

			service.Delete();
			Assert.IsFalse(service.Check(value));
		}

		[TestMethod]
		public void LocalMachineTest()
		{
			const string key = @"RunAtStartup.LocalMachineTest";
			const string value = @"1919810";
			var service = new StartupService(key, StartupType.LocalMachine);
			service.Delete();
			Assert.IsFalse(service.Check(value));

			service.Set(@"1919810");
			Assert.IsTrue(service.Check(value));

			service.Delete();
			Assert.IsFalse(service.Check(value));
		}

		[TestMethod]
		public void CheckTest()
		{
			const string key = @"RunAtStartup.CheckTest";
			const string value = @"114514";
			const string value2 = @"1919810";
			var service = new StartupService(key);
			service.Delete();
			Assert.IsFalse(service.Check(value));

			service.Set(value2);
			Assert.IsTrue(service.Check(value2));
			Assert.IsFalse(service.Check(value));

			service.Set(value);
			Assert.IsTrue(service.Check(value));
			Assert.IsFalse(service.Check(value2));

			service.Delete();
			Assert.IsFalse(service.Check(value));
		}
	}
}
