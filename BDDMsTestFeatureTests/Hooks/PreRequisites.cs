using BDDMsTestFeatureTests.BaseConfig;
using BDDMsTestFeatureTests.Data;
using BDDMsTestFeatureTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace BDDMsTestFeatureTests.Hooks
{
    [Binding]
    public sealed class PreRequisites : TestBase
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeScenario("Login")]
        public void BeforeScenarioLogin()
        {
            //TODO: implement logic that has to run before executing each scenario
            var registrationPage = new RegistrationPage(_driver);
            registrationPage.NavigateToRegistrationPage();
            var userInfo = new UserDto();
            var usercredentials = new UserLogin();
            registrationPage.EnterUserInformation(userInfo.FirstName, userInfo.LastName, userInfo.Street,userInfo.City, userInfo.State, userInfo.Zip, userInfo.Phone, userInfo.SSN, userInfo.Uname, userInfo.PassWord, userInfo.PassWord);
            registrationPage.SubmitRegistration();
            registrationPage.ConfirmRegistration(userInfo.Uname);
            registrationPage.LogOut();

        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}
