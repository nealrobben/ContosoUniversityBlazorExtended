using Bunit;
using Xunit;

namespace WebUI.Client.Test
{
    public class Class1
    {
        [Fact]
        public void Test1()
        {
            //Assert.Equal(1, 1);

            using var ctx = new TestContext();
        }
    }
}