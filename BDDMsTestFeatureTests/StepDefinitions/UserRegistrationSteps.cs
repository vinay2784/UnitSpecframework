using BDDMsTestFeatureTests.BaseConfig;
using BDDMsTestFeatureTests.Data;
using BDDMsTestFeatureTests.Helpers;
using BDDMsTestFeatureTests.Pages;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BDDMsTestFeatureTests.StepDefinitions
{
    [Binding]
    public class UserRegistrationSteps : TestBase
    {
        private RegistrationPage _registrationPage;
        private LoginPage _loginPage;
        private readonly UserRegistration userRegistration;
        private readonly UserDto userDto;


        public UserRegistrationSteps(UserRegistration userRegistration)
        {
            
            this.userRegistration = userRegistration;
            userDto = new UserDto();
            
        }

        [Given(@"User clicks on Registration link")]
        public void GivenUserClicksOnRegistrationLink()
        {
            _registrationPage = new RegistrationPage(_driver);
            _registrationPage.NavigateToRegistrationPage();
        }


        [Given(@"User Enter (.*),(.*),(.*),(.*),(.*),(.*),(.*),(.*),(.*),(.*),(.*)")]
        public void GivenUserEnter(string firstname, string lastname, string street, string city, string state, string zipcode, string phone, string ssn, string uname, string password, string rpassword)
        {
            firstname = userDto.FirstName;
            lastname = userDto.LastName;
            street = userDto.Street;
            city = userDto.City;
            state = userDto.State;
            zipcode = userDto.Zip;
            phone = userDto.Phone;
            ssn = userDto.SSN;
            uname = userDto.Uname;            
            password = userDto.PassWord;
            rpassword = userDto.PassWord;

            userRegistration.firstname = firstname;
            userRegistration.lastname = lastname;
            userRegistration.uname = uname;

            _registrationPage.EnterUserInformation(firstname, lastname, street, city, state, zipcode, phone, ssn, uname, password, rpassword); 
        }

        
        [When(@"User submit registration")]
        public void WhenUserSubmitRegistration()
        {
            _registrationPage.SubmitRegistration();
        }

        
        [Then(@"The welcome message displays to the user with (.*)")]
        public void ThenTheWelcomeMessageDisplaysToTheUserWith(string userid)
        {
            _registrationPage.ConfirmRegistration(userRegistration.uname);
        }

        
        [Then(@"User LogOut from the Application")]
        public void ThenUserLogOutFromTheApplication()
        {
            accountServicePage.LogOut();
        }

     
        [When(@"User login with")]
        public void WhenUserLoginWith(Table table)
        {
            _loginPage = new LoginPage(_driver);
            var userdetails = table.CreateInstance<UserLogin>();
            userdetails.uname = userDto.Uname;
            userdetails.password = userDto.PassWord;
            _loginPage.LoginIntoParaBank(userdetails.uname,userdetails.password);
        }


        [Then(@"Accounts OverView Page displays to the user\.")]
        public void ThenAccountsOverViewPageDisplaysToTheUser_()
        {
            _loginPage.ConfirmAccountOverView();
        }

    }
}
