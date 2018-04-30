using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class FrontPageTests
    {
        IApp app;
        Platform platform;

        public FrontPageTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void CO2EmissionCakeCanvasIsShown()
        {
            // For the Marked() method to work crossplatformly, the xaml element must have the AutomationId="someIdString" property set.
            AppResult[] results = app.WaitForElement(c => c.Marked("carbonFootprintCanvas"));
            Assert.IsNotEmpty(results);
        }

        [Test]
        public void WelcomeLabelDisplaysUsersFirstname()
        {
            
        }
    }
}
