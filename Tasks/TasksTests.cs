using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Tasks
{
    public class TasksTests
    {
        private const string appiumServer = "http://127.0.0.1:4723/wd/hub";
        private const string appLocation = @"C:\Google Tasks_2023.11.06.581929586.1-release_Apkpure.apk";
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;

        [SetUp]
        public void Setup()
        {
            this.options = new AppiumOptions() { PlatformName = "Android"};
            options.AddAdditionalCapability("app", appLocation);
            this.driver =new AndroidDriver<AndroidElement>(new Uri(appiumServer), options);
        }

        [TearDown]

        public void TearDown() 
        
        {  
            
            this.driver.Quit(); 
        
        }  

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}