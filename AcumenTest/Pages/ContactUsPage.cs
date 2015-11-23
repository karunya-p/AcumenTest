using AcumenTest.Core;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AcumenTest.Pages
{
    public class ContactUsPage
    {
        private readonly IWebDriver driver;

        public ContactUsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void NavigateToHomePage()
        {
            driver.Navigate().GoToUrl("http://www.acumenci.com/");
            var homePageTextLocator = By.XPath(@"//h3[text() = 'Welcome to Acumen']");
            driver.WaitUntilElementIsVisible(homePageTextLocator, 10);
        }

        public void NavigateToContactUsPage()
        {
            var contactUsTab = driver.FindElement(By.Id("menu-item-497"));
            contactUsTab.Click();
            var contactUsLocator = By.XPath(@"//h3[text() = 'Contact Us']");
            driver.WaitUntilElementIsVisible(contactUsLocator, 10);
        }

        public void SubmitTheContactUsFormWith(EndUserContactDetails endUserContactDetails)
        {
            EnterTheValueInTextField("your-name", endUserContactDetails.Name);
            EnterTheValueInTextField("YourCompany", endUserContactDetails.Company);
            EnterTheValueInTextField("YourLocation", endUserContactDetails.Location);
            EnterTheValueInTextField("Telephone", endUserContactDetails.Telephone);
            EnterTheValueInTextField("your-email", endUserContactDetails.Email);
            EnterTheValueInTextField("your-subject", endUserContactDetails.Subject);
            EnterTheValueInTextField("your-message", endUserContactDetails.YourMessage);

            ClickSubmit();
        }

        private void ClickSubmit()
        {
            var sendButton = driver.FindElement(By.XPath("//input[@type='submit' or @value='Send']"));
            sendButton.Click();
        }

        private void EnterTheValueInTextField(string nameAttribute, string valueIntheTextField)
        {
            var nameTextField = driver.FindElement(By.Name(nameAttribute));
            nameTextField.SendKeys(valueIntheTextField);
        }
    }
}