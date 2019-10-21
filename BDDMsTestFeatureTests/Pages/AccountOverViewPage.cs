using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDDMsTestFeatureTests.Pages
{
    public class AccountOverViewPage
    {
        protected IWebDriver _driver { get; set; }

        public AccountOverViewPage(IWebDriver driver) : base()
        {
            _driver = driver;
        }               

        public AccountOverViewPage LogOut()
        {
            var _btnLogout = _driver.FindElement(By.CssSelector("a[href*='logout']"));
            _btnLogout.Click();
            return this;
        }
        
        public AccountOverViewPage ConfirmAccountDetails()
        {
            var _tblAccount = _driver.FindElements(By.XPath("//table[@id='accountTable']/tbody/tr"));
            if (_tblAccount != null)
            {
                foreach(var row in _tblAccount)
                {
                    var accountDetails = row.FindElements(By.XPath("//td"));
                    Assert.IsTrue(accountDetails[0].Displayed);
                    Assert.IsTrue(accountDetails[0].Enabled);
                    Assert.IsTrue(accountDetails[0].TagName.Contains("a"));
                    Assert.IsTrue(accountDetails[1].Text.Contains("$"));
                    Assert.IsTrue(accountDetails[2].Text.Contains("$"));
                }
            }
            else            
                throw new Exception("Account details not exist for the user");
           
            
            return this;

        }
       
    }
}
