using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;

namespace EaAPP_Test_Project.Pages
{
    public class EmployeePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        // Store By locators
        private readonly By employeeListLinkLocator = By.LinkText("Employee List");
        private readonly By createNewButtonLocator = By.LinkText("Create New");
        private readonly By nameFieldLocator = By.Id("Name");
        private readonly By salaryFieldLocator = By.Id("Salary");
        private readonly By durationWorkedFieldLocator = By.Id("DurationWorked");
        private readonly By gradeDropdownLocator = By.Id("Grade");
        private readonly By emailFieldLocator = By.Id("Email");
        private readonly By submitButtonLocator = By.CssSelector(".btn");
        private readonly By searchFieldLocator = By.Name("searchTerm");
        private readonly By deleteLinkLocator = By.LinkText("Delete");

        public EmployeePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void GoToEmployeeList()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(employeeListLinkLocator)).Click();
            }
            catch (StaleElementReferenceException)
            {
                // Retry if stale element
                driver.FindElement(employeeListLinkLocator).Click();
            }
        }

        public void GoToCreateEmployeeForm()
        {
            GoToEmployeeList();
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(createNewButtonLocator)).Click();
            }
            catch (StaleElementReferenceException)
            {
                driver.FindElement(createNewButtonLocator).Click();
            }
        }

        public void FillEmployeeForm(string name, string salary, string duration, string grade, string email)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(nameFieldLocator)).SendKeys(name);
            driver.FindElement(salaryFieldLocator).SendKeys(salary);
            driver.FindElement(durationWorkedFieldLocator).SendKeys(duration);

            var gradeDropdown = driver.FindElement(gradeDropdownLocator);
            gradeDropdown.FindElement(By.XPath($".//option[. = '{grade}']")).Click();

            driver.FindElement(emailFieldLocator).SendKeys(email);

            // Wait for and try clicking Submit
            var submitBtn = wait.Until(ExpectedConditions.ElementToBeClickable(submitButtonLocator));
            try
            {
                submitBtn.Click();
            }
            catch (ElementClickInterceptedException)
            {
                // Use JS click as fallback
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", submitBtn);
            }
        }

        public void SearchEmployee(string name)
        {
            var searchField = wait.Until(ExpectedConditions.ElementIsVisible(searchFieldLocator));
            searchField.Clear();
            searchField.SendKeys(name);
            searchField.SendKeys(Keys.Enter);
        }

        public void DeleteEmployee()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(deleteLinkLocator)).Click();
            }
            catch (StaleElementReferenceException)
            {
                driver.FindElement(deleteLinkLocator).Click();
            }

            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(submitButtonLocator)).Click();
            }
            catch (ElementClickInterceptedException)
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                var submitBtn = driver.FindElement(submitButtonLocator);
                js.ExecuteScript("arguments[0].click();", submitBtn);
            }
        }
    }
}
