using BDDMsTestFeatureTests.Data;
using BDDMsTestFeatureTests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace BDDMsTestFeatureTests.Pages
{
    public class RegistrationPage : AccountOverViewPage
    {
        private WebDriverWait wait;
        public RegistrationPage(IWebDriver driver) : base(driver)
        {
        }
               

        public RegistrationPage NavigateToRegistrationPage()
        {
            var _lnkRegisterElem = _driver.FindElement(By.CssSelector("a[href*='register']"));
            _lnkRegisterElem.Click();

            var pageTitle = _driver.Title;
            Assert.IsTrue(pageTitle.Contains("Register"));
            return this;
        }

        public RegistrationPage EnterUserInformation(string firstname, string lastname, string street, string city, string state, string zipcode,string phone, string ssn, string uname, string password, string rpassword)
        {
            wait = new WebDriverWait(_driver,TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.UrlContains("register.htm"));            
                        
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[id*='firstName']")));
            var _txtboxFirstName = _driver.FindElement(By.CssSelector("input[id*='firstName']"));
            _txtboxFirstName.SendKeys(firstname);

            var _txtboxLastName = _driver.FindElement(By.CssSelector("input[id*='lastName']"));
            _txtboxLastName.SendKeys(lastname);

            var _txtboxAddr = _driver.FindElement(By.CssSelector("input[id*='street']"));
            _txtboxAddr.SendKeys(street);

            var _txtboxCity = _driver.FindElement(By.CssSelector("input[id*='city']"));
            _txtboxCity.SendKeys(city);

            var _txtboxState = _driver.FindElement(By.CssSelector("input[id*='state']"));
            _txtboxState.SendKeys(state);

            var _txtboxZipCode = _driver.FindElement(By.CssSelector("input[id*='zipCode']"));
            _txtboxZipCode.SendKeys(zipcode);

            var _txtboxPhone = _driver.FindElement(By.CssSelector("input[id*='phoneNumber']"));
            _txtboxPhone.SendKeys(phone);

            var _txtboxSSN = _driver.FindElement(By.CssSelector("input[id*='ssn']"));
            _txtboxSSN.SendKeys(ssn);

            var _txtboxUname = _driver.FindElement(By.CssSelector("input[id*='username']"));
            _txtboxUname.SendKeys(uname);

            var _txtboxPassword = _driver.FindElement(By.CssSelector("input[id*='password']"));
            _txtboxPassword.SendKeys(password);

            var _txtboxConfirm = _driver.FindElement(By.CssSelector("input[id*='repeatedPassword']"));
            _txtboxConfirm.SendKeys(rpassword);

            return this;

        }
        public RegistrationPage SubmitRegistration()
        {
            var _btnRegister = _driver.FindElement(By.CssSelector("input[value='Register']"));
            _btnRegister.Click();
            return this;
        }
        public AccountOverViewPage ConfirmRegistration(string userid)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1.title")));
            var confirmationElem = _driver.FindElement(By.CssSelector("h1.title"));

            var confirmationMsg = confirmationElem.Text;
            var successElm = _driver.FindElement(By.CssSelector("h1.title + p"));
            var successMsg = successElm.Text;

            Assert.IsTrue(confirmationMsg.Contains("Welcome"));
            Assert.IsTrue(confirmationMsg.Contains(userid));
            return new AccountOverViewPage(_driver);
        }
      
    }
}
