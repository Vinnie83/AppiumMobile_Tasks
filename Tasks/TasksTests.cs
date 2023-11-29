using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System.Diagnostics.Tracing;

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
            var taskName = "Pregled Ali";

            Thread.Sleep(2000); 
            
            var getStartedButton = driver.FindElementById("com.google.android.apps.tasks:id/welcome_get_started"); 
            getStartedButton.Click();

            var createTask = driver.FindElementByXPath("//android.widget.ImageButton[@content-desc=\"Create new task\"]");
            createTask.Click();

            Thread.Sleep(2000);

            var newTaskField = driver.FindElementById("com.google.android.apps.tasks:id/add_task_title");
            newTaskField.SendKeys(taskName);

            var saveButton = driver.FindElementById("com.google.android.apps.tasks:id/add_task_done");
            saveButton.Click();

            var createdTask = driver.FindElementByXPath("//android.widget.FrameLayout[@content-desc=\"Pregled Ali\"]/android.view.ViewGroup/android.widget.TextView");

            Thread.Sleep(2000);

            Assert.That(createdTask, Is.Not.Null);
            Assert.That(createdTask.Text, Is.EqualTo(taskName));

        }
    }
}