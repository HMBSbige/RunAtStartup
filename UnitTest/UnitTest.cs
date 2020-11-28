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
			var service = new StartupService(key);
			Assert.IsFalse(service.Check());

			service.Set(@"114514");
			Assert.IsTrue(service.Check());

			service.Delete();
			Assert.IsFalse(service.Check());
		}

		[TestMethod]
		public void LocalMachineTest()
		{
			const string key = @"RunAtStartup.LocalMachineTest";
			var service = new StartupService(key, StartupType.LocalMachine);
			Assert.IsFalse(service.Check());

			service.Set(@"1919810");
			Assert.IsTrue(service.Check());

			service.Delete();
			Assert.IsFalse(service.Check());
		}
	}
}
