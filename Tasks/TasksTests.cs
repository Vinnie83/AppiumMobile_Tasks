using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System.Diagnostics.Tracing;
using System.Security.Cryptography.X509Certificates;

namespace Tasks
{
    public class TasksTests
    {
        private const string appiumServer = "http://127.0.0.1:4723/wd/hub";
        private const string appLocation = @"C:\Google Tasks_2023.11.06.581929586.1-release_Apkpure.apk";
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;

        private string taskAli = "Pregled Ali"; 

        [OneTimeSetUp]
        public void Setup()
        {
            this.options = new AppiumOptions() { PlatformName = "Android"};
            options.AddAdditionalCapability("app", appLocation);
            this.driver =new AndroidDriver<AndroidElement>(new Uri(appiumServer), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var getStartedButton = driver.FindElementById("com.google.android.apps.tasks:id/welcome_get_started");
            getStartedButton.Click();

            if (IsTaskPresent(taskAli))
            {
                DeleteTask(taskAli);
            }
            
        }

        private void DeleteTask(string taskAli)
        {
            var createdTask = driver.FindElementByXPath("//android.widget.FrameLayout[@content-desc=\"Pregled Ali\"]/android.view.ViewGroup/android.widget.TextView");
            createdTask.Click();
            
            var moreOptionsButon = driver.FindElementByXPath("//android.widget.ImageView[@content-desc=\"More options\"]");
            moreOptionsButon.Click();

            var deleteOption = driver.FindElementById("com.google.android.apps.tasks:id/title");
            deleteOption.Click();   
        }

        private bool IsTaskPresent(string taskAli)
        {
            try
            {
                var presentTask = driver.FindElementByXPath("//android.widget.FrameLayout[@content-desc=\"Pregled Ali\"]/android.view.ViewGroup/android.widget.TextView");

                return presentTask != null;
            
            }
            
            catch(NoSuchElementException)
            {
                return false;

            }
                      
        }

        private void CreateTask(string taskAli)
        {
            

            var createTask = driver.FindElementByXPath("//android.widget.ImageButton[@content-desc=\"Create new task\"]");
            createTask.Click();

            Thread.Sleep(2000);

            var newTaskField = driver.FindElementById("com.google.android.apps.tasks:id/add_task_title");
            newTaskField.SendKeys("Pregled Ali");

            var saveButton = driver.FindElementById("com.google.android.apps.tasks:id/add_task_done");
            saveButton.Click();
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

            CreateTask(taskName);
            
            var createdTask = driver.FindElementByXPath("//android.widget.FrameLayout[@content-desc=\"Pregled Ali\"]/android.view.ViewGroup/android.widget.TextView");

            Thread.Sleep(2000);

            Assert.That(createdTask, Is.Not.Null);
            Assert.That(createdTask.Text, Is.EqualTo(taskName));

        }
    }
}