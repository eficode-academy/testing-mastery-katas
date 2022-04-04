// Copied from https://github.com/emilybache/Single-Sign-On-Kata

using Backend.BusinessLogic;
using Backend.sso;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class TestDoubleExercise
    {
        [TestMethod]
        public void ValidSSOTokenIsAccepted()
        {
            // TODO: use a test double for the SingleSignOnRegistry
            SingleSignOnRegistry registry = null;
            MyService service = new MyService(registry);
            SSOToken token = new SSOToken();
            Response response = service.HandleRequest(new Request("Foo", token));
            Assert.AreEqual("hello Foo!", response.GetText());
        }
    }
}
