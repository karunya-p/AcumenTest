using AcumenTest.Core;
using AcumenTest.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace AcumenTest.StepDefinitions
{
    [Binding]
    public class AcumenContactUsSteps
    {
        private readonly IWebDriver driver = new ChromeDriver();
        private ContactUsPage contactUsPage;
        [BeforeScenario()]
        public void BeforeScenario()
        {
            contactUsPage = new ContactUsPage(driver);
            driver.Manage().Window.Maximize();
        }

        [AfterScenario()]
        public void AfterScenario()
        {
            driver.Close();
            driver.Quit();
        }

        [Given(@"I am on the Acumen home page")]
        public void GivenIAmOnTheAcumenHomePage()
        {
            contactUsPage.NavigateToHomePage();
            Assert.That(driver.Title == "A Competitive Edge in Customer Management - Acumen");
        }

        [When(@"I navigate to the Contact Us page")]
        public void WhenINavigateToTheContactUsPage()
        {
            contactUsPage.NavigateToContactUsPage();
            Assert.That(driver.Title == "Contact Us - Acumenci");
        }

        [Given(@"I am on the Acumen Contact Us page")]
        public void GivenIAmOnTheAcumenContactUsPage()
        {
            GivenIAmOnTheAcumenHomePage();
            WhenINavigateToTheContactUsPage();
        }

        [When(@"I submit the contact form with the following details below")]
        public void WhenISubmitTheContactFormWithTheFollowingDetailsBelow(Table table)
        {
            var endUserContactDetails = new EndUserContactDetails
            {
                Name = table.Rows[0][0],
                Company = table.Rows[0][1],
                Location = table.Rows[0][2],
                Telephone = table.Rows[0][3],
                Email = table.Rows[0][4],
                Subject = table.Rows[0][5],
                YourMessage = table.Rows[0][6],
            };
            contactUsPage.SubmitTheContactUsFormWith(endUserContactDetails);
        }

        [Then(@"I should see successful submission message")]
        public void ThenIShouldSeeSuccessfulSubmissionMessage()
        {
            var successMessageElementLocator = By.XPath(@"//div[@class = 'wpcf7-response-output wpcf7-display-none wpcf7-mail-sent-ok']");
            driver.WaitUntilElementIsVisible(successMessageElementLocator, 20);
            var successMessageElement = driver.FindElement(successMessageElementLocator);
            Assert.That(successMessageElement.Text.Trim() == "Your message was sent successfully. Thank you and someone will be in contact with you shortly.");
        }


        [Then(@"I should see errors about the other required fields that are not filled")]
        public void ThenIShouldSeeErrorsAboutTheOtherRequiredFields()
        {
            var requiredFieldValidationErrorMessage = "Please fill the required field.";

            var summaryValidationErrorLocator = By.XPath("//div[@class='wpcf7-response-output wpcf7-display-none wpcf7-validation-errors']");
            driver.WaitUntilElementIsVisible(summaryValidationErrorLocator, 10);
            var summaryErrorMessage = driver.FindElement(summaryValidationErrorLocator);
            Assert.That(summaryErrorMessage.Text.Trim() == "Validation errors occurred. Please confirm the fields and submit it again.");

            var nameErrorMessage = driver.FindElement(By.XPath("//span[@class = 'wpcf7-form-control-wrap your-name']/span"));
            var companyErrorMessage = driver.FindElement(By.XPath("//span[@class = 'wpcf7-form-control-wrap YourCompany']/span"));
            var locationErrorMessage = driver.FindElement(By.XPath("//span[@class = 'wpcf7-form-control-wrap YourLocation']/span"));
            Assert.That(nameErrorMessage.Text.Trim() == requiredFieldValidationErrorMessage);
            Assert.That(companyErrorMessage.Text.Trim() == requiredFieldValidationErrorMessage);
            Assert.That(locationErrorMessage.Text.Trim() == requiredFieldValidationErrorMessage);
        }



        [Then(@"I should see validation error message '(.*)'")]
        public void ThenIShouldSeeValidationErrorMessage(string expectedEmailValidationError)
        {
            var emailValidationErrorLocator = By.XPath("//span[@class = 'wpcf7-form-control-wrap your-email']/span");
            driver.WaitUntilElementIsVisible(emailValidationErrorLocator, 10);
            var emailValidationMessageElement = driver.FindElement(emailValidationErrorLocator);
            Assert.That(emailValidationMessageElement.Text.Trim() == expectedEmailValidationError);
        }

        [Then(@"I should see no errors in the email field")]
        public void ThenIShouldSeeNoErrorsInTheEmailField()
        {
            Assert.Throws<NoSuchElementException>(() =>
            {
                var emailValidationMessageLocator = By.XPath("//span[@class = 'wpcf7-form-control-wrap your-email']/span");
                driver.FindElement(emailValidationMessageLocator);
            });

        }

        [Then(@"I should see an accurate address as follows")]
        public void ThenIShouldSeeAnAccurateAddressAsFollows(Table table)
        {
            var contactAddressDetails = new ContactAddressDetails()
            {
                CompanyName = table.Rows[0][0].Trim(),
                StreetAddress = table.Rows[0][1].Trim(),
                Town = table.Rows[0][2].Trim(),
                CountyWithPostCode = table.Rows[0][3].Trim(),
                Country = table.Rows[0][4].Trim(),
                ClientEnquiresTelephone = table.Rows[0][5].Trim(),
                ClientEnquiresEmail = table.Rows[0][6].Trim(),
                SupportEnquiresTelephone = table.Rows[0][7].Trim(),
                SupportEnquiresEmail = table.Rows[0][8].Trim(),
            };

            var companyNameElement = driver.FindElement(By.XPath(@"//h6[text() = 'Acumen Commercial Insights Ltd']"));
            Assert.That(companyNameElement.Text.Trim() == contactAddressDetails.CompanyName);

            var addressElement = driver.FindElement(By.XPath(@"//p[text() = '26 George Street']"));
            Assert.IsTrue(addressElement.Text.Trim().Contains(contactAddressDetails.StreetAddress));
            Assert.IsTrue(addressElement.Text.Trim().Contains(contactAddressDetails.Town));
            Assert.IsTrue(addressElement.Text.Trim().Contains(contactAddressDetails.CountyWithPostCode));
            Assert.IsTrue(addressElement.Text.Trim().Contains(contactAddressDetails.Country));

            var clientEnquiriesElement = driver.FindElement(By.XPath(@"//p[text() = 'Client enquiries']"));
            Assert.IsTrue(clientEnquiriesElement.Text.Trim().Contains(contactAddressDetails.ClientEnquiresTelephone));


            var clientEnquiryEmailElement = driver.FindElement(By.XPath(@"//a[text()= 'info@acumenci.com']"));
            Assert.That(clientEnquiryEmailElement.Text.Trim() == contactAddressDetails.ClientEnquiresEmail);

            var supportEnquiryEmailElement = driver.FindElement(By.XPath(@"//a[text()= 'support@acumenci.com']"));
            Assert.That(supportEnquiryEmailElement.Text.Trim() == contactAddressDetails.SupportEnquiresEmail);

            var supportEnquiriesElement = driver.FindElement(By.XPath(@"//p[text() = 'Support enquiries']"));
            Assert.IsTrue(supportEnquiriesElement.Text.Trim().Contains(contactAddressDetails.SupportEnquiresTelephone));

        }


        [Then(@"I should be able to see Google map widget showing address")]
        public void ThenIShouldBeAbleToSeeGoogleMapWidgetShowingAddress()
        {
            var mapHeaderElement = driver.FindElement(By.XPath(@"//h5[text() = 'Acumen on the map']"));
            Assert.That(mapHeaderElement.Text.Trim() == "Acumen on the map");

            var googleMapIframe = driver.FindElement(By.TagName("iframe"));
            Assert.IsTrue(googleMapIframe.Displayed);
        }

        [Then(@"I should see how to find us section")]
        public void ThenIShouldSeeHowToFindUsSection()
        {
            var howToFindUsElement = driver.FindElement(By.XPath(@"//h5[text() = 'HOW TO FIND US']"));
            Assert.That(howToFindUsElement.Text.Trim() == "HOW TO FIND US");

            var sectionElement = driver.FindElement(By.XPath(@"//h5[text() = 'HOW TO FIND US']/following-sibling::div"));
            Assert.IsTrue(sectionElement.Displayed);
            Assert.IsTrue(sectionElement.Text.Trim().Contains("BY AIR"));
            Assert.IsTrue(sectionElement.Text.Trim().Contains("BY RAIL"));
            Assert.IsTrue(sectionElement.Text.Trim().Contains("BY CAR"));
            Assert.IsTrue(sectionElement.Text.Trim().Contains("ON FOOT"));
        }

    }
}
