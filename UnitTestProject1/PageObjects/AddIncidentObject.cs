using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace OHSConnect.PageObjects
{
    class AddIncidentObject
    {
        public AddIncidentObject(IWebDriver webdriver)
        {
            PageFactory.InitElements(webdriver, this);
        }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""1""]/a")]
        [CacheLookup]
        public IWebElement AddMenu { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""1_0""]/a")]
        [CacheLookup]
        public IWebElement AddanIncidentMenu { get; set; }

        [FindsBy(How = How.Id, Using = "name_1_0_DateTimeofIncident")]
        [CacheLookup]
        public IWebElement DateTimeIncident { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""name_1_1_DateTimeReported""]")]
        [CacheLookup]
        public IWebElement DateTimeReported { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""orignalDiv20""]/div/div[2]/div/div/div/multi-select-tree/div/div[1]/div/div[1]")]
        [CacheLookup]
        public IWebElement BusinessUnitDrpDwn { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""orignalDiv20""]/div/div[2]/div/div/div/multi-select-tree/div/div[2]/ul/tree-item[1]/li/div/div[2]/div/label")]
        [CacheLookup]
        public IWebElement IshanTestOption { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""orignalDiv30""]/div/div[2]/div/div/div/input")]
        [CacheLookup]
        public IWebElement SpecificLocationtxt { get; set; }

        [FindsBy(How = How.Id, Using = "select_4_0")]
        [CacheLookup]
        public IWebElement IncidentTypeDrpDwn { get; set; }

        [FindsBy(How = How.XPath, Using = @"(//div[@id=""orignalDiv50""]/descendant::textarea[@name=""name_5_0_BriefDescriptionofIncident""])")]
        [CacheLookup]
        public IWebElement BriefDescriptiontxt { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""orignalDiv50""]/div/div[2]/div/div/div/div/div/button[1]")]
        [CacheLookup]
        public IWebElement NotifiableIncidentYes { get; set; }

        [FindsBy(How = How.XPath, Using = @"(//button[contains(text(),'No')])[1]")]
        [CacheLookup]
        public IWebElement NotifiableIncidentNo { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""orignalDiv80""]/div/div[2]/div/div/div/div/div/button[1]")]
        [CacheLookup]
        public IWebElement EmergencyServicesattendYes { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""orignalDiv80""]/div/div[2]/div/div/div/div/div/button[2]")]
        [CacheLookup]
        public IWebElement EmergencyServicesattendNo { get; set; }

        [FindsBy(How = How.XPath, Using = @"(//div[@id=""orignalDiv120""]/descendant::input[@type=""text""])")]
        [CacheLookup]
        public IWebElement GivenNametxt { get; set; }

        [FindsBy(How = How.XPath, Using = @"(//div[@id=""orignalDiv121""]/descendant::input[@type=""text""])")]
        [CacheLookup]
        public IWebElement Surnametxt { get; set; }

        [FindsBy(How = How.XPath, Using = @"(//div[@id=""orignalDiv130""]/descendant::textarea[@name=""name_13_0_DescriptionofInjury""])")]
        [CacheLookup]
        public IWebElement DescriptionofInjurytxt { get; set; }

        [FindsBy(How = How.XPath, Using = @"(//button[contains(text(),'Add another record')])[1]")]
        [CacheLookup]
        public IWebElement AddAnotherRecordbtn { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""orignalDiv90""]/div/div[3]/div/div/textarea")]
        [CacheLookup]
        public IWebElement ImmediateActionTakentxt { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""orignalDiv100""]/div/div[3]/div/div/textarea")]
        [CacheLookup]
        public IWebElement Suggestedactiontxt { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""orignalDiv240""]/div/div[2]/div/div/div/input")]
        [CacheLookup]
        public IWebElement WitnessNametxt { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""orignalDiv241""]/div/div[2]/div/div/div/input")]
        [CacheLookup]
        public IWebElement WitnessContactNumbertxt { get; set; }

        [FindsBy(How = How.XPath, Using = @"(//span[contains(text(),'Select or search...')])[1]")]
        [CacheLookup]
        public IWebElement ReportedByDrpDwn { get; set; }

        [FindsBy(How = How.XPath, Using = @"//div[contains(text(),'Black, Robert')]")]
        [CacheLookup]
        public IWebElement ReportedByDrpDwnOption { get; set; }        

        [FindsBy(How = How.XPath, Using = @"(//span[contains(text(),'Select or search...')])[2]")]
        [CacheLookup]
        public IWebElement CompletedByDrpDwn { get; set; }

        [FindsBy(How = How.XPath, Using = @"(//div[contains(text(),'Black')])[2]")]
        [CacheLookup]
        public IWebElement CompletedByDrpDwnoption { get; set; }

        [FindsBy(How = How.XPath, Using = @"(//span[contains(text(),'Owner')])[2]")]
        [CacheLookup]
        public IWebElement OwnerDrpDwn { get; set; }

        [FindsBy(How = How.XPath, Using = @"//span[contains(text(),'ISJUN-PER-8')]")]
        [CacheLookup]
        public IWebElement OwnerDrpDwnOption { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[@id=""formValidate""]/div/div[3]/div/div/button[2]")]
        [CacheLookup]
        public IWebElement SaveandFinishbtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Home')]")]
        [CacheLookup]
        public IWebElement HomeMenu { get; set; }
    }
}