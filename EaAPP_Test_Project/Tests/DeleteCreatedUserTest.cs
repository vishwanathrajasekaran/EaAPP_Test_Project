using EaAPP_Test_Project.Pages;
using EaAPP_Test_Project.TestDatas;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
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
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");
            driver = new ChromeDriver(options);
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
            loginPage.GoToLoginPage();
            loginPage.Login(TestData.Username, TestData.Password);

            employeePage.GoToEmployeeList();
            employeePage.SearchEmployee(TestData.EmployeeName);

            var deleteLinks = driver.FindElements(By.LinkText("Delete"));

            if (deleteLinks.Count == 0)
            {
                Console.WriteLine($"No employee found with name '{TestData.EmployeeName}' to delete.");
            }
            else
            {
                while (true)
                {
                    deleteLinks = driver.FindElements(By.LinkText("Delete"));
                    if (deleteLinks.Count == 0)
                        break;

                    deleteLinks[0].Click();
                    var confirmButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                                            .Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".btn")));
                    confirmButton.Click();

                    // Wait for redirect back to employee list and search box
                    new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                        .Until(ExpectedConditions.ElementIsVisible(By.Name("searchTerm")));

                    // Refresh search to update list after delete
                    employeePage.SearchEmployee(TestData.EmployeeName);
                }

                Console.WriteLine($"All employees with name '{TestData.EmployeeName}' deleted.");
            }
        }

    }
}

