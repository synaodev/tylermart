using Xunit;

using TylerMart.Testing.Services;

namespace TylerMart.Testing {
	public class UnitTest {
		[Fact]
		public void Test() {
			DatabaseService service = new DatabaseService();
		}
	}
}
