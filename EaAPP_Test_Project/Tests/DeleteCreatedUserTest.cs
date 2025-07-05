using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using EaAPP_Test_Project.Pages;
using EaAPP_Test_Project.TestDatas;
using System;
using System.Collections.ObjectModel;

namespace EaAPP_Test_Project.Tests
{
    [TestFixture]
    public class DeleteCreatedUserTest
    {
        private IWebDriver driver;
        private LoginPage loginPage;
        private EmployeePage employeePage;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Size = new System.Drawing.Size(1464, 868);
            driver.Navigate().GoToUrl("http://eaapp.somee.com/");

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            loginPage = new LoginPage(driver);
            employeePage = new EmployeePage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void DeleteCreatedUser()
        {
            // 1. Login
            loginPage.GoToLoginPage();
            loginPage.Login(TestData.Username, TestData.Password);

            // 2. Navigate to Employee List
            employeePage.GoToEmployeeList();

            // 3. Search for the employee by name
            employeePage.SearchEmployee(TestData.EmployeeName);

            // 4. Find all matching Delete links for the search results
            ReadOnlyCollection<IWebElement> deleteLinks = driver.FindElements(By.LinkText("Delete"));

            if (deleteLinks.Count == 0)
            {
                Console.WriteLine($"No employee found with name '{TestData.EmployeeName}' to delete.");
            }
            else
            {
                // Loop and delete all matching employees one by one
                while (deleteLinks.Count > 0)
                {
                    deleteLinks[0].Click();  // Click Delete for first item
                    wait.Until(d => d.FindElement(By.CssSelector(".btn"))).Click(); // Confirm delete button

                    // Wait until redirected back to employee list
                    wait.Until(d => d.FindElement(By.Name("searchTerm")));

                    // Search again to refresh list
                    employeePage.SearchEmployee(TestData.EmployeeName);

                    // Refresh delete links collection
                    deleteLinks = driver.FindElements(By.LinkText("Delete"));
                }

                Console.WriteLine($"All employees with name '{TestData.EmployeeName}' deleted.");
            }
        }
    }
}
