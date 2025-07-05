using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using EaAPP_Test_Project.Pages;
using EaAPP_Test_Project.TestDatas;

namespace EaAPP_Test_Project.Tests
{
    [TestFixture]
    public class CreateEmployeeTest
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
        public void CreateAndSearchEmployee()
        {
            // Instantiate Page Objects
            var loginPage = new LoginPage(driver);
            var employeePage = new EmployeePage(driver);

            // ✅ Check if login is needed
            if (IsLoginRequired())
            {
                loginPage.LoginLinkClick();
                loginPage.PerformLogin(TestData.Username, TestData.Password);
            }

            // ✅ Proceed to create employee
                employeePage.GoToCreateEmployeeForm();
                employeePage.FillEmployeeForm(
                name: TestData.EmployeeName,
                salary: TestData.EmployeeSalary,
                duration: TestData.EmployeeDuration,
                grade: TestData.EmployeeGrade,
                email: TestData.EmployeeEmail
            );
            employeePage.SearchEmployee(TestData.EmployeeName);

            // ✅ Optional Assert
            Assert.IsTrue(driver.PageSource.Contains(TestData.EmployeeName), "Employee was not created or found.");
        }

        // ✅ Reusable check: Is login needed?
        private bool IsLoginRequired()
        {
            try
            {
                // Try to find the login link (visible only when logged out)
                return driver.FindElement(By.Id("loginLink")).Displayed;
            }
            catch (NoSuchElementException)
            {
                // loginLink not found => already logged in
                return false;
            }
        }
    }
}
