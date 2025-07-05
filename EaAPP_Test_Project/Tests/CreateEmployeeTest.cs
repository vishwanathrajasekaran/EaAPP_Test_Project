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
        public void CreateAndSearchEmployee()
        {
            var loginPage = new LoginPage(driver);
            var employeePage = new EmployeePage(driver);

            loginPage.NavigateToHomePage();
            loginPage.ClickLoginLink();
            loginPage.PerformLogin(TestData.Username, TestData.Password);

            employeePage.GoToCreateEmployeeForm();
            employeePage.FillEmployeeForm(
                TestData.EmployeeName,
                TestData.EmployeeSalary,
                TestData.EmployeeDuration,
                TestData.EmployeeGrade,
                TestData.EmployeeEmail
            );

            employeePage.SearchEmployee(TestData.EmployeeName);
        }
    }
}
