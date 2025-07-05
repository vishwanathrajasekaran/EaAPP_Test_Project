using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using EaAPP_Test_Project.Pages;
using EaAPP_Test_Project.TestDatas;

namespace EaAPP_Test_Project.Tests
{
    [TestFixture]
    public class LoginTest
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose(); // Optional but good
            }
        }


        [Test]
        public void ValidLoginTest()
        {
            var loginPage = new LoginPage(driver);

            loginPage.NavigateToHomePage(); // Step 1: Navigate to home
            loginPage.ClickLoginLink();     // Step 2: Click login link
            loginPage.PerformLogin(TestData.Username, TestData.Password); // Step 3: Login
        }
    }
}
