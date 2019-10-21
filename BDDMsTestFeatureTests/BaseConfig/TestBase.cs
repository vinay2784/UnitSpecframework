using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BDDMsTestFeatureTests.ExtensionMethods;
using BDDMsTestFeatureTests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;


namespace BDDMsTestFeatureTests.BaseConfig
{
    [Binding]
    public class TestBase 
    {
        private string startupPath = Directory.GetCurrentDirectory();
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        protected static IWebDriver _driver { get; set; }
        protected static AccountOverViewPage accountServicePage { get; set; }

        //Instance of extents reports

        private static ExtentTest _feature;
        private static ExtentTest _scenario;
        private static ExtentReports _extent;


        private static string PathReport;
        private static string localPath;


        [BeforeTestRun]
        public static void BeforeTestRun()
        {

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            PathReport = $"{Environment.CurrentDirectory}ExtentReport.html";

            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://parabank.parasoft.com");
            accountServicePage = new AccountOverViewPage(_driver);

            var reporter = new ExtentHtmlReporter(PathReport);
            _extent = new ExtentReports();
            _extent.AttachReporter(reporter);

        }
        
        [BeforeFeature]
        public static void CreateFeature()
        {
            _feature = _extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }
        [BeforeScenario]
        public static void CreateScenario()
        {
            _scenario = _feature.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }
        [AfterStep]
        public static void InsertReportingSteps()
        {
            switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
            {
                case StepDefinitionType.Given:
                    _scenario.StepDefinitionGiven();
                    break;

                case StepDefinitionType.Then:
                    _scenario.StepDefinitionThen();
                    break;

                case StepDefinitionType.When:
                    _scenario.StepDefinitionWhen();
                    break;
            }
        }
        public static string MakeScreenshotAfterStep()
        {
            var takesScreenshot = _driver as ITakesScreenshot;
            if (takesScreenshot != null)
            {
                var screenshot = takesScreenshot.GetScreenshot();
                localPath = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileNameWithoutExtension(Path.GetTempFileName())) + ".jpg";
                screenshot.SaveAsFile(localPath, ScreenshotImageFormat.Jpeg);
                return localPath;

            }
            return null;
        }

        
        [AfterTestRun]
        public static void AfterTestRun()
        {            
            _extent.Flush();
            _driver?.Quit();
            try
            {
                ThreadStart threadStart = delegate () { SendEmailAsync();  };
                Thread thread = new Thread(threadStart);
                thread.Start();
            }
            catch (Exception)
            {

                throw;
            }
            if (_driver != null)
            {
                _driver.Dispose();
                _driver = null;
            }
        }

        public static void SendEmailAsync()
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("vinayagan.pothiraja@gmail.com");
                mail.To.Add("vinayagan.pothiraja@gmail.com");
                mail.Subject = "Extent Report.";
                mail.Body = "Extent Report.";
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                mail.Attachments.Add(new Attachment(PathReport));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {

                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(mail.From.Address, "tamil@2015");
                    smtp.EnableSsl = true;
                    smtp.Timeout = 10000;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(mail);

                }
            }
        }
       
    }
}
