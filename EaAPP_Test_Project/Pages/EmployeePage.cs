using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace EaAPP_Test_Project.Pages
{
    public class EmployeePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public EmployeePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Removed cached IWebElement properties

        public void GoToEmployeeList()
        {
            var employeeListLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Employee List")));
            employeeListLink.Click();
        }

        public void GoToCreateEmployeeForm()
        {
            GoToEmployeeList();
            var createNewButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Create New")));
            createNewButton.Click();
        }

        public void FillEmployeeForm(string name, string salary, string duration, string grade, string email)
        {
            var nameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Name")));
            var salaryField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Salary")));
            var durationWorkedField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("DurationWorked")));
            var gradeDropdown = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Grade")));
            var emailField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Email")));
            var submitButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".btn")));

            nameField.Clear();
            nameField.SendKeys(name);

            salaryField.Clear();
            salaryField.SendKeys(salary);

            durationWorkedField.Clear();
            durationWorkedField.SendKeys(duration);

            var option = gradeDropdown.FindElement(By.XPath($"//option[. = '{grade}']"));
            option.Click();

            emailField.Clear();
            emailField.SendKeys(email);

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);
            submitButton.Click();
        }

        public void SearchEmployee(string name)
        {
            var searchField = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("searchTerm")));
            searchField.Clear();
            searchField.SendKeys(name);
            searchField.SendKeys(Keys.Enter);
        }

        public void DeleteEmployee()
        {
            var deleteLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Delete")));
            deleteLink.Click();

            var confirmButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".btn")));
            confirmButton.Click();
        }
    }
}
