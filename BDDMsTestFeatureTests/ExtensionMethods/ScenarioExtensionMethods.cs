using System; 
using AventStack.ExtentReports; 
using AventStack.ExtentReports.Gherkin.Model;
using BDDMsTestFeatureTests.BaseConfig;
using TechTalk.SpecFlow; 
using TechTalk.SpecFlow.Bindings;


namespace BDDMsTestFeatureTests.ExtensionMethods
{
    public static class ScenarioExtensionMethods
    {
        private static ExtentTest _step;
        private static string _screenshotPath;

        private static ExtentTest CreateScenario(ExtentTest extent, StepDefinitionType stepDefinitionType)
        {
            var scenarioStepContext = ScenarioStepContext.Current.StepInfo.Text;
            
            switch (stepDefinitionType)
            {
                case StepDefinitionType.Given:
                    return extent.CreateNode<Given>(scenarioStepContext);
                    
                case StepDefinitionType.Then:
                    return extent.CreateNode<Then>(scenarioStepContext);

                case StepDefinitionType.When:
                    return extent.CreateNode<When>(scenarioStepContext);
                default:
                    throw new ArgumentOutOfRangeException(nameof(stepDefinitionType), stepDefinitionType, null);
            }
        }
        private static void CreateScenarioFailOrError(ExtentTest extent, StepDefinitionType stepDefinitionType)
        {
            var error = ScenarioContext.Current.TestError;

            if (error.InnerException == null)
            {
                _step = CreateScenario(extent, stepDefinitionType).Error(error.Message);
            }
            else
            {
                _step = CreateScenario(extent, stepDefinitionType).Fail(error.InnerException);
            }
            
            _screenshotPath = TestBase.MakeScreenshotAfterStep();
            _step.AddScreenCaptureFromPath(_screenshotPath);
            
        }
        public static void StepDefinitionGiven(this ExtentTest extent)
        {
            if (ScenarioContext.Current.TestError == null)
            {
                _step = CreateScenario(extent, StepDefinitionType.Given);
                _screenshotPath = TestBase.MakeScreenshotAfterStep();
                _step.AddScreenCaptureFromPath(_screenshotPath);
            }              
            else
                CreateScenarioFailOrError(extent, StepDefinitionType.Given);
        }
        public static void StepDefinitionWhen(this ExtentTest extent)
        {
            if (ScenarioContext.Current.TestError == null)
            {
                _step = CreateScenario(extent, StepDefinitionType.When);
                _screenshotPath = TestBase.MakeScreenshotAfterStep();
                _step.AddScreenCaptureFromPath(_screenshotPath);
            }
                
            else
                CreateScenarioFailOrError(extent, StepDefinitionType.When);

            
        }

        public static void StepDefinitionThen(this ExtentTest extent)
        {
            if (ScenarioContext.Current.TestError == null)
            {
                _step = CreateScenario(extent, StepDefinitionType.Then);
                _screenshotPath = TestBase.MakeScreenshotAfterStep();
                _step.AddScreenCaptureFromPath(_screenshotPath);
            }               
            else
                CreateScenarioFailOrError(extent, StepDefinitionType.Then);

        }

    }
}
