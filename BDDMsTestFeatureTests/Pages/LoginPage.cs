using NUnit.Framework;
using OpenQA.Selenium;

namespace BDDMsTestFeatureTests.Pages
{
    public class LoginPage : AccountOverViewPage
    {
        
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }
        public LoginPage LoginIntoParaBank(string uname,string password)
        {
            var title = _driver.Title;
            Assert.IsTrue(title.Contains("ParaBank"));
            Assert.IsTrue(title.Contains("Welcome"));
            Assert.IsTrue(title.Contains("Online"));

            var LoginSection = _driver.FindElement(By.CssSelector("#loginPanel"));
            Assert.IsTrue(LoginSection.Displayed);

            var _txtuname = _driver.FindElement(By.Name("username"));
            _txtuname.SendKeys(uname);

            var _txtpassword = _driver.FindElement(By.Name("password"));
            _txtpassword.SendKeys(password);

            var _btnLogin = _driver.FindElement(By.CssSelector("input[value='Log In']"));
            _btnLogin.Click();
            return this;
        }
        public AccountOverViewPage ConfirmAccountOverView()
        {
            
            var _txtOverview = _driver.FindElement(By.CssSelector("h1.title")).Text;
            var _tblAccount = _driver.FindElement(By.CssSelector("#accountTable"));
            Assert.IsTrue(_txtOverview.Contains("Accounts"));
            Assert.IsTrue(_txtOverview.Contains("Overview"));
            Assert.IsTrue(_tblAccount.Displayed);

            return new AccountOverViewPage(_driver);
        }
    }
}
