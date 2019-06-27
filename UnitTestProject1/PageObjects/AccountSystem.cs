using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace OHSConnect.PageObjects
{
    class AccountSystem
    {
        public AccountSystem(IWebDriver webdriver)
        {
            PageFactory.InitElements(webdriver, this);
        }

        [FindsBy(How = How.XPath, Using = "//h4[contains(text(),'Azure_21JUN2019_Test_Account (AZU98)')]")]
        [CacheLookup]
        public IWebElement SelectAccount { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/section/div[1]/div/div/div[2]/div/div[2]/div[2]/a/img")]
        [CacheLookup]
        public IWebElement SelectSystem { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""ulSection""]/li[6]/div[1]/a/span")]
        [CacheLookup]
        public IWebElement UserIcon { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""ulSection""]/li[6]/div[1]/ul/li[1]/a/span/span")]
        [CacheLookup]
        public IWebElement LoggedInUserName { get; set; }

        [FindsBy(How = How.XPath, Using = @"//div[contains(text(),'Select an Account')]")]
        [CacheLookup]
        public IWebElement SelectAccountLabel { get; set; }
    }
}