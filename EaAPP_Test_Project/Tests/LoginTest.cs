using EaAPP_Test_Project.Pages;
using EaAPP_Test_Project.TestDatas;  // make sure this is added
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EaAPP_Test_Project.Tests  // ensure this matches your folder structure
{
    [TestFixture]
    public class LoginTest
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");
            driver = new ChromeDriver(options);
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        [Test]
        public void ValidLoginTest()
        {
            var loginPage = new LoginPage(driver);
            loginPage.NavigateToHomePage();

            // Use TestData static class here
            loginPage.PerformLogin(TestData.Username, TestData.Password);
        }
    }
}
