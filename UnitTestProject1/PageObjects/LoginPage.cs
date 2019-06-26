using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace OHSConnect.PageObjects
{
    class LoginPage
    {
        private IWebDriver webdriver;

        public LoginPage(IWebDriver webdriver)
        {
            this.webdriver = webdriver;
            PageFactory.InitElements(webdriver, this);
        }

        [FindsBy(How = How.Id, Using = "account_username")]
        [CacheLookup]
        public IWebElement UserNameTextBox { get; set; }

        [FindsBy(How = How.Id, Using = "account_password")]
        [CacheLookup]
        public IWebElement PasswordTextBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type = 'submit' and text()='Login']")]
        [CacheLookup]
        public IWebElement LoginButton { get; set; }
    }
}